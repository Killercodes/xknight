using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using xKnight.Models;
using xKnight.WebCrawler;
using xKnight.WebCrawler.Event;

namespace xKnight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private delegate void UpdateUserInterface(CrawlAnnouncedEventArgs e);

        CrawlSetting _crawlSetting;
        Host _host;

        public MainWindow()
        {
            InitializeComponent();
            _crawlSetting = new CrawlSetting();
            _host = new Host();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            lsvCrawlStatus.Items.Clear();

            _host.HostName = txtAddress.Text;
            _crawlSetting.MaxDepth = 1000;
            Crawler crawler = new Crawler(_crawlSetting, _host, int.Parse(txtThreadNumber.Text));

            crawler.CrawlCompleted += crawler_CrawlCompleted;
            crawler.CrawlAnnounced += crawler_CrawlAnnounced;
            crawler.CrawlStarted += crawler_CrawlStarted;

            crawler.Crawl();
        }

        void crawler_CrawlStarted(object sender, WebCrawler.Event.CrawlStartedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void crawler_CrawlAnnounced(object sender, WebCrawler.Event.CrawlAnnouncedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new UpdateUserInterface(OnCrawlAnnounced), e);
            
        }

        private void OnCrawlAnnounced(CrawlAnnouncedEventArgs e)
        {
            lblTotalDownloadedLinks.Content = e.CrawlAnnounceItem.CrawlingSharedResource.TotalPagesDownloaded;
            lblTotalForms.Content = e.CrawlAnnounceItem.CrawlingSharedResource.TotalFormsFound;
            lblTotalUniqueForms.Content = e.CrawlAnnounceItem.CrawlingSharedResource.TotalUniqueFormsFound;
            lblTotalUniqueLinks.Content = e.CrawlAnnounceItem.CrawlingSharedResource.TotalUniqueLinksFound;
            lblTotalLinks.Content = e.CrawlAnnounceItem.CrawlingSharedResource.TotalLinksFound;

            lsvCrawlStatus.Items.Add(e.CrawlAnnounceItem);

        }

        void crawler_CrawlCompleted(object sender, WebCrawler.Event.CrawlCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
