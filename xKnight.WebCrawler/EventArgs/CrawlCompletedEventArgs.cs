using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.WebCrawler;

namespace xKnight.WebCrawler.Event
{
    public class CrawlCompletedEventArgs
    {
        public Crawler Crawler{get;private set;}
        
        public CrawlCompletedEventArgs(Crawler crawler)
        {
            this.Crawler = crawler;
        }
    }
}
