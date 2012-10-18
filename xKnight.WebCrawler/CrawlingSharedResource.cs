using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.WebCrawler
{
    public class CrawlingSharedResource
    {
        internal Queue<Webpage> SharedQueue {get; private set;}

        internal object SharedLock { get; private set; }

        internal HashSet<string> SharedPageHash { get; private set; }

        internal HashSet<string> SharedFormHash { get; private set; }

        public CrawlSetting CrawlerSetting { get; private set; }

        public int TotalLinksFound { get; private set; }

        public int TotalUniqueLinksFound { get { return SharedPageHash.Count; } }

        public int TotalFormsFound { get; private set; }

        public int TotalUniqueFormsFound { get {return SharedFormHash.Count;} }

        public int TotalPagesDownloaded { get; private set; }

        internal Host Host { get; set; }

        public CrawlingSharedResource(CrawlSetting crawlerSetting, Host host, Queue<Webpage> sharedQueue, object sharedLock, HashSet<string> sharedPageHash, HashSet<string> sharedFormHash)
        {
            CrawlerSetting = crawlerSetting;
            Host = host;
            SharedQueue = sharedQueue;
            SharedLock = sharedLock;
            SharedFormHash = sharedFormHash;
            SharedPageHash = sharedPageHash;
        }

        internal void IncrementTotalPagesDownloaded()
        {
            lock(SharedLock)
            {
                TotalPagesDownloaded++;
            }
        }

        internal void AddTotalFormsFound(int num)
        {
            lock(SharedLock)
            {
                TotalFormsFound+=num;
            }
        }

        internal void AddTotalLinksFound(int num)
        {
            lock(SharedLock)
            {
                TotalLinksFound+=num;
            }
        }
    }
}
