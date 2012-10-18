using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.WebCrawler.Event
{
    public class CrawlAnnounceItem
    {
        public string Description { get; private set; }
        public Webpage Page { get; private set; }
        public CrawlStatus CrawlStatus { get; private set; }
        public DateTime DateTime { get; private set; }
        public CrawlingSharedResource CrawlingSharedResource { get; private set; }

        public CrawlAnnounceItem(Webpage page, CrawlStatus crawlStatus, string description, DateTime dateTime, CrawlingSharedResource crawlingSharedResource)
        {
            this.CrawlingSharedResource = crawlingSharedResource;
            this.Description = description;
            this.Page = page;
            this.CrawlStatus = crawlStatus;
            this.DateTime = dateTime;
        }
    }
}
