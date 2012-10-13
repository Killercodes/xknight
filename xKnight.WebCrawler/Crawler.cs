using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.WebCrawler
{
    public class Crawler
    {
        private List<HostPage> _lstPage; // queue for downloading pages;
        private List<Host> _lstHost;
        private Models.CrawlSetting _crawlSetting;
        
        public Crawler()
        {
            _crawlSetting=new CrawlSetting();
            _lstHost=new List<Host>();
        }

        public CrawlSetting CrawlSetting
        {
            get { return _crawlSetting; }
            set { _crawlSetting = value; }
        }

        public List<Host> Hosts 
        { 
            get { return _lstHost; }
        }

        public bool Start()
        {
            if(_lstHost.Count==0)
                throw  new InvalidOperationException("No Host Specified");

            if (ValidateCrawlSetting(_crawlSetting))
            {
                
            }
        }

        private bool ValidateCrawlSetting(Models.CrawlSetting CrawlSetting)
        {
            return true;
        }

        private void CrawlHost(Host host)
        {
            
        }

        private void DownloadPage(HostPage page)
        {
            
        }

        private HostPage[] ExtractLinks(HostPage page)
        {
            
        }

        private bool IsDownloaded(HostPage page)
        {
            
        }
    }
}
