using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.Attacking
{
    public abstract class Attacker
    {
        #region Events

        public delegate void AttackCompletedEventHandler(object sender, AttackCompletedEventArgs e);
        public event AttackCompletedEventHandler AttackCompleted;

        public delegate void AttackStartedEventHandler(object sender, AttackStartedEventArgs e);
        public event AttackStartedEventHandler AttackStarted;

        public delegate void AttackAnnouncedEventHandler(object sender, AttackAnnouncedEventArgs e);
        public event AttackAnnouncedEventHandler AttackAnnounced;

        #endregion

        #region Fields

        protected int _numberOfThreads;
        protected Attack _attack;
        protected int _aliveAgents;
        protected object _lock = new object();

        #endregion

        #region Constructors

        public Attacker(Attack attack, int numberOfThreads)
        {
            _attack = attack;
            _numberOfThreads = numberOfThreads;
        }

        #endregion

        #region Interface

        public abstract void Attack();

        #endregion

        #region Event Handler

        protected void agent_AgentAttackAnnounced(object sender, AttackAnnouncedEventArgs e)
        {
            OnAttackAnnounced(e);
        }

        protected void agent_AgentAttackStarted(object sender, EventArgs e)
        {
            lock (_lock)
            {
                _aliveAgents++;
                if (_aliveAgents== _numberOfThreads)
                {
                    OnAttackStarted(this);
                }
            }
        }

        protected void agent_AgentAttackCompleted(object sender, EventArgs e)
        {
            lock (_lock)
            {
                _aliveAgents--;
                if (_aliveAgents == 0)
                {
                    _attack.FinishTime = DateTime.Now;
                    DataLayer.Save(_attack);

                    OnAttackCompleted(this);
                }
            }
        }

        #endregion

        #region Events Raiser

        private void OnAttackCompleted(Attacker attacker)
        {
            if (AttackCompleted != null)
            {
                AttackCompleted(this, new AttackCompletedEventArgs(attacker));
            }
        }

        private void OnAttackStarted(Attacker attacker)
        {
            if (AttackStarted != null)
            {
                AttackStarted(this, new AttackStartedEventArgs(attacker));
            }
        }

        private void OnAttackAnnounced(AttackAnnouncedEventArgs e)
        {
            if (AttackAnnounced != null)
            {
                AttackAnnounced(this, new AttackAnnouncedEventArgs(e.AnnounceItem));
            }
        }

        #endregion
    }
}
