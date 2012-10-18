using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.WebCrawler
{
    public enum CrawlStatus
    {
        DownloadingStarted,
        DownloadingFinished,
        ExtractingLinksStarted,
        ExtractingLinksFinished,
        ExtractingFormsStarted,
        ExtractingFormsFinished,
        DownloadingHalted
    }
}
