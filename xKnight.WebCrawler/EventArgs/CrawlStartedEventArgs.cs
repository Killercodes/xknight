using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.WebCrawler;

namespace xKnight.WebCrawler.Event
{
    public class CrawlStartedEventArgs
    {
        public Crawler Crawler{get;private set;}

        public CrawlStartedEventArgs(Crawler crawler)
        {
            this.Crawler = crawler;
        }
    }
}
