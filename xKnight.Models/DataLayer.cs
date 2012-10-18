using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Models
{
    public class DataLayer
    {
        public static void Save(Webpage page)
        {
            xKnightEntities context = new xKnightEntities();
            if (page.Id == 0)
            {
                context.Webpages.AddObject(page);
                context.SaveChanges();
            }
            else
            {
                context.Webpages.Attach(page);
                context.ObjectStateManager.ChangeObjectState(page, System.Data.EntityState.Modified);
            }
            context.Dispose();
        }

        public static void Save(Host host)
        {
            xKnightEntities context = new xKnightEntities();
            if (host.Id == 0)
            {
                context.Hosts.AddObject(host);
                context.SaveChanges();
            }
            else
            {
                context.Hosts.Attach(host);
                context.ObjectStateManager.ChangeObjectState(host, System.Data.EntityState.Modified);
            }
            context.Dispose();
        }

        public static void Save(CrawlSetting crawlSetting)
        {
            xKnightEntities context = new xKnightEntities();
            if (crawlSetting.Id == 0)
            {
                context.CrawlSettings.AddObject(crawlSetting);
                context.SaveChanges();
            }
            else
            {
                context.CrawlSettings.Attach(crawlSetting);
                context.ObjectStateManager.ChangeObjectState(crawlSetting, System.Data.EntityState.Modified);
            }
            context.Dispose();
        }

        public static Host[] GetCrawlRunHosts(object _crawlId)
        {
            throw new NotImplementedException();
        }

        public static void Save(XAttack xAttack)
        {
            throw new NotImplementedException();
        }

        public static Form[] GetHostsForms(Host[] _hosts)
        {
            throw new NotImplementedException();
        }

        public static void Save(Attack attack)
        {
            throw new NotImplementedException();
        }
    }
}
