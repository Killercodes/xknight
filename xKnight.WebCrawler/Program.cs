using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            CrawlSetting setting = new CrawlSetting();
            setting.MaxDepth = 1000;
            
            Host[] hosts=new Host[1];
            hosts[0] = new Host();
            hosts[0].HostName = "http://salamandroid.ir";

            Crawler crawler = new Crawler(setting,hosts,10);
            crawler.Crawl();

            Console.ReadLine();
        }
    }
}
