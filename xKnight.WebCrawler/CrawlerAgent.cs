using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using xKnight.Models;
using xKnight.WebCrawler.Event;

namespace xKnight.WebCrawler
{
    internal class CrawlerAgent
    {

        #region Delegates
        private delegate void CrawlAsyncCaller();
        #endregion

        #region Events

        public delegate void CrawlAgentCompletedEventHandler(object sender, EventArgs e);
        public event CrawlAgentCompletedEventHandler CrawlAgentCompleted;
        
        public delegate void CrawlAgentStartedEventHandler(object sender, EventArgs e);
        public event CrawlAgentStartedEventHandler CrawlAgentStarted;

        public event xKnight.WebCrawler.Crawler.CrawlAnnouncedEventHandler CrawlAnnounced;

        #endregion

        #region Fields

        private static int _numberOfDownloaders = 0;
        private CrawlingSharedResource _sharedResource;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crawlerSetting">Settings for this job</param>
        /// <param name="host">the host to do crawling on it</param>
        /// <param name="threadsNumber">number of threads for crawling the specified host</param>
        public CrawlerAgent(CrawlingSharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Setting which Crawling is based on it
        /// </summary>
        public CrawlingSharedResource CrawlingSharedResource
        {
            get { return _sharedResource; }
        }

        #endregion 

        #region Interface

        public void Crawl()
        {        
            AddDomainPageToQueue();

            Webpage page = null;

            while ((page = GetNotVistedPage()) != null)
            {
                Interlocked.Increment(ref _numberOfDownloaders);

                DownloadPage(page);
                _sharedResource.IncrementTotalPagesDownloaded();

                if (page.Html != null)
                {
                    Form[] forms=ExtractForms(page);

                    AddFormsToPage(page,forms);

                    DataLayer.Save(page);

                    Webpage[] pages = ExtractLinks(page);

                    AddPagesToQueue(page, pages);
                }

                Interlocked.Decrement(ref _numberOfDownloaders);
            }
        }
        public void CrawlAsync()
        {
            CrawlAsyncCaller crawlAsyncCaller = new CrawlAsyncCaller(Crawl);
            AsyncCallback callback = new AsyncCallback(OnCrawlCompelete);

            OnCrawlAgentStarted();
            crawlAsyncCaller.BeginInvoke(callback,null);
        }

        #endregion

        #region Private Methods

        private void AddFormsToPage(Webpage page, Form[] forms)
        {
            lock (_sharedResource.SharedLock)
            {
                for (int i = 0; i < forms.Length; i++)
                {                    
                    string id=forms[i].Action + ":" + forms[i].Method;
                    for (int j = 0; j < forms[i].FormElements.Count; j++)
                    {
                        id+=":"+forms[i].FormElements.ElementAt(j).Name+":"+forms[i].FormElements.ElementAt(j).Value;
                    }

                    byte[] hashData = Encoding.UTF8.GetBytes(id);

                    if (!_sharedResource.SharedFormHash.Contains(id))
                    {
                        page.Forms.Add(forms[i]);
                        _sharedResource.SharedFormHash.Add(id);
                    }
                    else
                        Console.WriteLine("Duplicated Form");
                }
            }
        }

        private void AddDomainPageToQueue()
        {
            lock (_sharedResource.SharedLock)
            {
                if (!_sharedResource.SharedPageHash.Contains(_sharedResource.Host.HostName))
                {
                    Webpage page = new Webpage();
                    page.Depth = 0;
                    page.Url = _sharedResource.Host.HostName;
                    page.HostId = _sharedResource.Host.Id;

                    _sharedResource.AddTotalLinksFound(1);
                    _sharedResource.SharedPageHash.Add(page.Url);
                    _sharedResource.SharedQueue.Enqueue(page);
                }
            }
        }

        private void AddPagesToQueue(Webpage parent, Webpage[] pages)
        {
            if (pages == null)
                return;

            for (int i = 0; i < pages.Length; i++)
            {
                if (Uri.IsWellFormedUriString(pages[i].Url, UriKind.Absolute))
                {
                    Uri uri = new Uri(pages[i].Url);
                    Uri dns = new Uri(_sharedResource.Host.HostName);
                    if (uri.Host == dns.Host)
                    {
                        if (!_sharedResource.SharedQueue.Any(p => p.Url == pages[i].Url)
                            && _sharedResource.CrawlerSetting.MaxDepth > parent.Depth)
                        {
                            lock (_sharedResource.SharedLock)
                            {
                                if (!_sharedResource.SharedPageHash.Contains(pages[i].Url))
                                {
                                    _sharedResource.SharedPageHash.Add(pages[i].Url);
                                    _sharedResource.SharedQueue.Enqueue(pages[i]);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// returns a page which has not visted yet
        /// </summary>
        /// <returns>Not visted page, if all pages where visited returns null</returns>
        private Webpage GetNotVistedPage()
        {
            while (_sharedResource.SharedQueue.Count == 0 && _numberOfDownloaders != 0) Thread.Sleep(1000) ;

            Webpage page = null;
            lock (_sharedResource.SharedLock)
            {
                page = _sharedResource.SharedQueue.Count != 0 ? _sharedResource.SharedQueue.Dequeue() : null;
                Console.WriteLine(String.Format("Queue : {0}, Thread : {1}", _sharedResource.SharedQueue.Count, Thread.CurrentThread.ManagedThreadId));
            }

            return page;

        }

        /// <summary>
        /// Downloads  contents and save it
        /// </summary>
        /// <param name="page"></param>
        private void DownloadPage(Webpage page)
        {

            CrawlAnnounceItem item = new CrawlAnnounceItem(page, CrawlStatus.DownloadingStarted, null, DateTime.Now, _sharedResource);
            OnCrawlAnnounced(item);

            if (page.Url.EndsWith(".jpg")
                || page.Url.EndsWith(".jpeg")
                || page.Url.EndsWith(".zip")
                || page.Url.EndsWith(".rar")
                || page.Url.EndsWith(".png")
                || page.Url.EndsWith(".exe")
                || page.Url.EndsWith(".gif")
                || page.Url.EndsWith(".mp3")
                || page.Url.EndsWith(".wma")
                || page.Url.EndsWith(".pdf")
                || page.Url.EndsWith(".wav")
                || page.Url.EndsWith(".bmp")
                || page.Url.EndsWith(".apk"))
            {
                page.Html = null;
                page.DateTime = DateTime.Now;

                item = new CrawlAnnounceItem(page, CrawlStatus.DownloadingHalted, "این آدرس محتوی متن نمی باشد.", DateTime.Now, _sharedResource);
                OnCrawlAnnounced(item);

                return;
            }

            try
            {
                HttpWebRequest request = WebRequest.Create(page.Url) as HttpWebRequest;
                request.Timeout = 100000;
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                request.AllowAutoRedirect = true;
                request.KeepAlive = false;

                if (page.Url.Contains("rar"))
                    Console.WriteLine();

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (
                        (response.StatusCode != HttpStatusCode.NotFound
                        || response.StatusCode != HttpStatusCode.BadGateway
                        || response.StatusCode != HttpStatusCode.BadRequest
                        || response.StatusCode != HttpStatusCode.Forbidden
                        || response.StatusCode != HttpStatusCode.GatewayTimeout
                        || response.StatusCode != HttpStatusCode.Gone
                        || response.StatusCode != HttpStatusCode.InternalServerError
                        || response.StatusCode != HttpStatusCode.NotAcceptable)
                        && (response.ContentType.Contains("text/html"))
                        )
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                            page.Html = sr.ReadToEnd();

                        item = new CrawlAnnounceItem(page, CrawlStatus.DownloadingFinished, null, DateTime.Now, _sharedResource);
                        OnCrawlAnnounced(item);
                    }
                    else
                    {
                        item = new CrawlAnnounceItem(page, CrawlStatus.DownloadingHalted, "خطایی در حین بارگذاری صفحه رخ داد", DateTime.Now, _sharedResource);
                        OnCrawlAnnounced(item);
                        page.Html = null;
                    }

                    page.DateTime = DateTime.Now;
                }
            }
            catch(WebException ex)
            {
                page.Html = null;
                page.DateTime = DateTime.Now;

                HttpWebResponse response = ex.Response as HttpWebResponse;

                if(response!=null)
                    item = new CrawlAnnounceItem(page, CrawlStatus.DownloadingHalted,response.StatusCode +" "+ response.StatusDescription + "خطایی در حین بارگذاری صفحه رخ داد", DateTime.Now, _sharedResource);
                else
                    item = new CrawlAnnounceItem(page, CrawlStatus.DownloadingHalted,"خطایی در حین بارگذاری صفحه رخ داد", DateTime.Now, _sharedResource);
                
                OnCrawlAnnounced(item);
            }
        }

        private Webpage[] ExtractLinks(Webpage parent)
        {
            CrawlAnnounceItem item = new CrawlAnnounceItem(parent, CrawlStatus.ExtractingLinksStarted, null, DateTime.Now, _sharedResource);
            OnCrawlAnnounced(item);

            List<Webpage> Webpages = new List<Webpage>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(parent.Html);
            htmlDocument.OptionFixNestedTags = true;

            foreach (HtmlNode link in htmlDocument.DocumentNode.Descendants("a")
                .Where(d => d.Attributes.Contains("href")))
            {

                Webpage page = new Webpage();
                page.Depth = parent.Depth + 1;
                page.HostId = parent.HostId;

                string uri = link.Attributes["href"].Value;

                if(Uri.IsWellFormedUriString(uri,UriKind.Absolute))
                    page.Url = uri;
                else if (Uri.IsWellFormedUriString(uri,UriKind.Relative))
                    page.Url = UnifyUri(parent, uri);

                if (page.Url != null)
                {
                    Uri href = new Uri(page.Url);
                    if (string.IsNullOrEmpty(href.Fragment))
                    {
                        page.Url = page.Url.ToLower();
                        page.RefererId = parent.Id;
                        page.Depth = parent.Depth + 1;
                        page.HostId = parent.HostId;

                        Webpages.Add(page);
                    }
                }

            }

            _sharedResource.AddTotalLinksFound(Webpages.Count);

            item = new CrawlAnnounceItem(parent, CrawlStatus.ExtractingLinksFinished, string.Format("این صفحه دارای {0} لینک می باشد.", Webpages.Count), DateTime.Now,_sharedResource);
            OnCrawlAnnounced(item);

            return Webpages.ToArray();
        }

        private string UnifyUri(Webpage basePage,string relativeUri)
        {
            try
            {
                Uri uri = new Uri(new Uri(basePage.Url), relativeUri);
                return uri.ToString();
            }
            catch
            {
                return null;
            }
        }

        private Form[] ExtractForms(Webpage page)
        {

            CrawlAnnounceItem item = new CrawlAnnounceItem(page, CrawlStatus.ExtractingFormsStarted, null, DateTime.Now, _sharedResource);
            OnCrawlAnnounced(item);

            List<Form> _formLst = new List<Form>();
            HtmlNode.ElementsFlags.Remove("form");

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page.Html);

            HtmlNode root = htmlDocument.DocumentNode;

            foreach (HtmlNode formNode in root.Descendants("form"))
            {
                Form form = new Form();
                HtmlAttribute att = formNode.Attributes["action"];
                string uri = (att == null || att.Value == "" || att.Value.StartsWith("#") ? page.Url : att.Value);

                if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                    form.Action = uri;
                else if (Uri.IsWellFormedUriString(uri, UriKind.Relative))
                    form.Action = UnifyUri(page, uri);

                form.Method = formNode.Attributes["method"].Value;

                if (form.Action != null)
                {
                    foreach (HtmlNode inputNode in formNode.Descendants("input"))
                    {
                        FormElement element = new FormElement();
                        if (inputNode.Attributes.Any(a => a.Name == "name"))
                            element.Name = inputNode.Attributes["name"].Value;
                        else
                            element.Name = "";

                        if (inputNode.Attributes.Any(a => a.Name == "value"))
                            element.Value = inputNode.Attributes["value"].Value;
                        else
                            element.Value = "";

                        element.Type = inputNode.Attributes["type"].Value;

                        form.FormElements.Add(element);
                    }

                    _formLst.Add(form);
                }
            }

            _sharedResource.AddTotalFormsFound(_formLst.Count);

            item = new CrawlAnnounceItem(page, CrawlStatus.ExtractingFormsFinished, string.Format("این صفحه دارای {0} فرم می باشد.", _formLst.Count), DateTime.Now, _sharedResource);
            OnCrawlAnnounced(item);

            return _formLst.ToArray();
        }

        #endregion

        #region EventHandlers

        private void OnCrawlCompelete(IAsyncResult result)
        {
            OnCrawlCompeleted();
        }

        private void OnCrawlAgentStarted()
        {
            if (CrawlAgentStarted != null)
                CrawlAgentStarted(this, new EventArgs());
        } 

        private void OnCrawlCompeleted()
        {
            if (CrawlAgentCompleted != null)
                CrawlAgentCompleted(this, new EventArgs());
        }

        private void OnCrawlAnnounced(CrawlAnnounceItem item)
        {
            if (CrawlAnnounced != null)
            {
                CrawlAnnounced(this, new CrawlAnnouncedEventArgs(item));
            }
        }

        #endregion
    }
}
