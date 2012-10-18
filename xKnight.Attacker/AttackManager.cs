using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.Attacking
{
    public class AttackManager
    {
        
        #region Fields
      
        private int _crawlId;
        private int _numberOfThreads;


        #endregion

        #region Constructors

        public AttackManager(int crawlId,int numberOfThreads)
        {
            _numberOfThreads = numberOfThreads;
            _crawlId=crawlId;
        }

        #endregion

        #region Interface

        public void Attack()
        {
            Host[] hosts = DataLayer.GetCrawlRunHosts(_crawlId);

            if (UseSimpleXssAttacker)
            {
                Attack attack = new Attack();
                attack.CrawlSettingId = _crawlId;
                DataLayer.Save(attack);

                SimpleXssAttacker simpleXssAttacker = new SimpleXssAttacker(attack, _numberOfThreads,hosts);
                simpleXssAttacker.AttackAnnounced += Attacker_AttackAnnounced;
                simpleXssAttacker.AttackCompleted += Attacker_AttackCompleted;
                simpleXssAttacker.AttackStarted += Attacker_AttackStarted;
                attack.StartTime = DateTime.Now;
                simpleXssAttacker.Attack();
                
                DataLayer.Save(attack);
            }

            if (UseEncodedXssAttacker)
            {
                Attack attack = new Attack();
                attack.CrawlSettingId = _crawlId;
                attack.StartTime = DateTime.Now;
                DataLayer.Save(attack);

                EncodedXssAttacker encodedXssAttacker = new EncodedXssAttacker(attack, _numberOfThreads, hosts);
                encodedXssAttacker.AttackAnnounced += Attacker_AttackAnnounced;
                encodedXssAttacker.AttackCompleted += Attacker_AttackCompleted;
                encodedXssAttacker.AttackStarted += Attacker_AttackStarted;

                encodedXssAttacker.Attack();
            }
        }

        #endregion

        #region EventHandler

        void Attacker_AttackStarted(object sender, AttackStartedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Attacker_AttackCompleted(object sender, AttackCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Attacker_AttackAnnounced(object sender, AttackAnnouncedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        public bool UseEncodedXssAttacker { get; set; }
        public bool UseSimpleXssAttacker { get; set; }

        #endregion
    }
}
