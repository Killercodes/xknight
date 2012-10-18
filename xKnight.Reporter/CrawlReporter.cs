using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.WebCrawler;
using xKnight.WebCrawler.Event;

namespace xKnight.Reporter
{
    public class CrawlReporter
    {
        /*
        #region Interface

        public void ReportStart(Crawler crawler)
        {
            OnCrawlStarted(crawler);
        }

        public void ReportComplete(Crawler crawler)
        {
            OnCrawlCompleted(crawler);
        }

        public void ReportAnnounce(CrawlAnnounceItem item)
        {
            OnCrawlAnnounced(item);
        }

        #endregion
         */ 

        /*
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
            if (CrawlCompleted != null)
            {
                CrawlCompleted(this, new CrawlCompletedEventArgs(crawler));
            }
        }

        private void OnCrawlAnnounced(CrawlAnnounceItem item)
        {
            if (CrawlAnnounced != null)
            {
                CrawlAnnounced(this, new CrawlAnnouncedEventArgs(item));
            }
        }

        #endregion
         */ 
    }
}
