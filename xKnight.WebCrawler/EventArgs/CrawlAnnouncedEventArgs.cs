using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.WebCrawler.Event
{
    public class CrawlAnnouncedEventArgs
    {
        public CrawlAnnounceItem CrawlAnnounceItem { get; private set; }

        public CrawlAnnouncedEventArgs(CrawlAnnounceItem crawlAnnounceItem)
        {
            this.CrawlAnnounceItem = crawlAnnounceItem;
        }
    }
}
