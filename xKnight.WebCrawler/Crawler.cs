using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using xKnight.Models;

namespace xKnight.WebCrawler
{
    public class Crawler
    {
        #region Fields

        /// <summary>
        /// queue for downloading pages;
        /// it must be static to be thread safe
        /// </summary>
 
        private static Queue<HostPage> _queuePage; 

        private Models.CrawlSetting _crawlSetting;
        private int _threadsNumber;
        private Models.Host _host;
        private static object _lockObject = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crawlerSetting">Settings for this job</param>
        /// <param name="host">the host to do crawling on it</param>
        /// <param name="threadsNumber">number of threads for crawling the specified host</param>
        public Crawler(CrawlSetting crawlerSetting, Host host, int threadsNumber = 1)
        {
            _crawlSetting = crawlerSetting;
            _threadsNumber = threadsNumber;
            _host = host;
            _queuePage = Queue<HostPage>;
            
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
        public Host Host
        {
            get { return _host; }
        }


        #endregion 

        #region Interface

        public void Crawl()
        {
            Thread[] _threads = new Thread[_threadsNumber];

            for (int i = 0; i < _threadsNumber; i++)
			{
                _threads[i] = new Thread();
                //_threads[i]
			}
        }

        public void CrawlAsync()
        {

        }

        #endregion

        private void StartAgent()
        {
            HostPage page = new HostPage();

            while ((page = GetNotVistedPage() != null))
            {
                DownloadPage(page);
                
                HostPage[] pages= ExtractLinks(page);

                AddPagesToQueue(pages);
            }
        }

        private void AddPagesToQueue(HostPage parent,HostPage[] pages)
        {
            lock (_lockObject)
            {
                for (int i = 0; i < pages.Length; i++)
                {
                    if(_queuePage.Where(p=>p.Url!=pages[i].Url)
                        && _crawlSetting.MaxDepth>parent.Depth)
                    {
                        pages[i].Depth = parent.Depth + 1;
                        pages[i].HostId = parent.Id;
                        
                        _queuePage.Enqueue(pages[i]);
                    }
                }
            }
        }

        /// <summary>
        /// returns a page which has not visted yet
        /// </summary>
        /// <returns>Not visted page, if all pages where visited returns null</returns>
        private HostPage GetNotVistedPage()
        {
            return _queuePage.Count!=0 ? _queuePage.Dequeue() : null;
        }

        private void DownloadPage(HostPage page)
        {
            WebClient webClient=new WebClient();
            webClient.Headers[HttpRequestHeader.UserAgent]="";
            page.Html = html=webClient.DownloadString(page.Url)

        }

        private HostPage[] ExtractLinks(HostPage page)
        {
            return null;
        }
    }
}
