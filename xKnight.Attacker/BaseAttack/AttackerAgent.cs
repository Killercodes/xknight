using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{
    public abstract class AttackerAgent
    {
        internal delegate void AttackAsyncCaller();

        #region Events

        public delegate void AgentAttackStartedEventHandler(object sender, EventArgs e);
        public event AgentAttackStartedEventHandler AgentAttackStarted;

        public delegate void AgentAttackCompletedEventHander(object sender, EventArgs e);
        public event AgentAttackCompletedEventHander AgentAttackCompleted;

        public delegate void AgentAttackAnnouncedEventHandler(object sender, AttackAnnouncedEventArgs e);
        public event AgentAttackAnnouncedEventHandler AgentAttackAnnounced;

        #endregion

        #region Interface

        public abstract void Attack();

        public abstract void AttackAsync();

        #endregion

        #region Event Handlers

        protected void OnAgentAttackStarted()
        {
            if (AgentAttackStarted != null)
                AgentAttackStarted(this, new EventArgs());
        }

        protected void OnAgentAttackCompleted(IAsyncResult result)
        {
            OnAgentAttackCompleted();
        }

        protected void OnAgentAttackCompleted()
        {
            if (AgentAttackCompleted != null)
                AgentAttackCompleted(this, new EventArgs());
        }

        protected void OnAgentAttackAnnounced(AttackAnnounceItem announceItem)
        {
            if (AgentAttackAnnounced != null)
                AgentAttackAnnounced(this, new AttackAnnouncedEventArgs(announceItem));
        }

        #endregion
    }
}
