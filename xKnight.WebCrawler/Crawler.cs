using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using xKnight.Models;
using xKnight.WebCrawler.Event;

namespace xKnight.WebCrawler
{
    public class Crawler
    {

        #region Events

        public delegate void CrawlCompletedEventHandler(object sender, CrawlCompletedEventArgs e);
        public event CrawlCompletedEventHandler CrawlCompleted;

        public delegate void CrawlStartedEventHandler(object sender, CrawlStartedEventArgs e);
        public event CrawlStartedEventHandler CrawlStarted;

        public delegate void CrawlAnnouncedEventHandler(object sender, CrawlAnnouncedEventArgs e);
        public event CrawlAnnouncedEventHandler CrawlAnnounced;

        #endregion

        #region Fields

        /// <summary>
        /// queue for downloading pages;
        /// it must be static to be thread safe
        /// </summary>

        private object _lock = new object();
        private Models.CrawlSetting _crawlSetting;
        private Host[] _hosts;
        private int _threadsNumber;
        Dictionary<Host,int> _aliveAgentsDic=new Dictionary<Host,int>();
        Dictionary<Host,CrawlerAgent[]> _agentsDic=new Dictionary<Host,CrawlerAgent[]>();

        #endregion
        
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crawlerSetting">Settings for this job</param>
        /// <param name="host">the host to do crawling on it</param>
        /// <param name="threadsNumber">number of threads for crawling the specified host</param>
        public Crawler(CrawlSetting crawlerSetting, Host[] hosts, int threadsNumber = 1)
        {
            _crawlSetting = crawlerSetting;
            _threadsNumber = threadsNumber;

            for (int i = 0; i < hosts.Length; i++)
            {
                if (!Uri.IsWellFormedUriString(hosts[i].HostName, UriKind.Absolute))
                {
                    throw new InvalidDataException(string.Format("invalid host name : {0}",hosts[i].HostName));
                }
            }

            _hosts = hosts;

        }

        public Crawler(CrawlSetting crawlSetting, Host host, int threadsNumber=1)
            :this(crawlSetting,new Host[]{host},threadsNumber)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Setting which Crawling is based on it
        /// </summary>
        public CrawlSetting CrawlSetting
        {
            get { return _crawlSetting; }
        }

        /// <summary>
        /// The Host which crawling is do on it
        /// </summary>
        public Host[] Hosts
        {
            get { return _hosts; }
        }

        #endregion
            
        #region Interface

        public void Crawl()
        {
            _crawlSetting.StartTime = DateTime.Now;

            DataLayer.Save(_crawlSetting);

            for (int i = 0; i < _hosts.Length; i++)
            {
                Queue<Webpage> sharedQueue = new Queue<Webpage>();
                object sharedLock = new object();
                HashSet<string> sharedPageHash = new HashSet<string>();
                HashSet<string> sharedFormHash = new HashSet<string>();

                CrawlingSharedResource sharedResource = new CrawlingSharedResource(_crawlSetting, _hosts[i], sharedQueue, sharedLock, sharedPageHash, sharedFormHash);
                CrawlerAgent[] agents = new CrawlerAgent[_threadsNumber];

                _aliveAgentsDic.Add(_hosts[i], 0);
                _agentsDic.Add(_hosts[i], agents);
                _hosts[i].StartTime = DateTime.Now;
                _hosts[i].CrawlId = _crawlSetting.Id;

                DataLayer.Save(_hosts[i]);

                for (int j = 0; j < _threadsNumber; j++)
                {
                    agents[j] = new CrawlerAgent(sharedResource);
                    agents[j].CrawlAgentCompleted += agent_CrawlAgentCompleted;
                    agents[j].CrawlAnnounced += agent_CrawlAnnounced;
                    agents[j].CrawlAgentStarted += Crawler_CrawlAgentStarted;
                    agents[j].CrawlAsync();
                }
            }
        }

        #endregion

        #region EventHandlers

        private void OnCrawlStarted(Crawler crawler)
        {
            if (CrawlStarted != null)
            {
                CrawlStarted(this, new CrawlStartedEventArgs(crawler));
            }
        }

        private void OnCrawlCompleted(Crawler crawler)
        {
            Console.WriteLine(crawler._crawlSetting.StartTime.Value.ToShortTimeString());
            Console.WriteLine(crawler._crawlSetting.FinishTime.Value.ToShortTimeString());
            if (CrawlCompleted != null)
            {
                CrawlCompleted(this, new CrawlCompletedEventArgs(crawler));
            }
        }

        private void OnCrawlAnnounced(CrawlAnnounceItem item)
        {
            Console.WriteLine(String.Format("{0}-{1}-{2}-{3}", item.Page.Url, item.CrawlStatus.ToString()
                , item.Description != null ? item.Description : "", item.DateTime.ToShortTimeString()));

            if (CrawlAnnounced != null)
            {
                CrawlAnnounced(this, new CrawlAnnouncedEventArgs(item));
            }
        }

        void agent_CrawlAnnounced(object sender, CrawlAnnouncedEventArgs e)
        {
            OnCrawlAnnounced(e.CrawlAnnounceItem);
        }

        void agent_CrawlAgentCompleted(object sender, EventArgs e)
        {
            CrawlerAgent agent = sender as CrawlerAgent;
            Host host = agent.CrawlingSharedResource.Host;
            
            lock (_lock)
            {
                _aliveAgentsDic[host]--;
                if (_aliveAgentsDic[host] == 0)
                {
                    _agentsDic.Remove(host);
                    host.FinishTime = DateTime.Now;

                    DataLayer.Save(host);

                    if (_agentsDic.Count == 0)
                    {
                       _crawlSetting.FinishTime = DateTime.Now;
                       DataLayer.Save(_crawlSetting);
                       OnCrawlCompleted(this);
                    }
                }
            }
        }

        void Crawler_CrawlAgentStarted(object sender, EventArgs e)
        {
            CrawlerAgent agent = sender as CrawlerAgent;
            Host host = agent.CrawlingSharedResource.Host;

            lock (_lock)
            {
                _aliveAgentsDic[host]++;
                if (_aliveAgentsDic.All(kv => kv.Value == _threadsNumber))
                {
                    OnCrawlStarted(this);
                }
            }
        }

        #endregion
    }
}
