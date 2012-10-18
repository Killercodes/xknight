using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.Attacking
{
    public class EncodedXssAttacker : Attacker
    {
        #region Fields

        private Host[] _hosts;
        
        #endregion

        #region Constructors

        public EncodedXssAttacker(Attack attack, int numberOfThreads, Host[] hosts)
            :base(attack,numberOfThreads)
        {
            _hosts = hosts;
        }

        #endregion

        #region Interface

        public override void Attack()
        {
            Form[] forms=DataLayer.GetHostsForms(_hosts);

            Queue<Form> sharedQueue = new Queue<Form>();
            foreach (var form in forms)
                sharedQueue.Enqueue(form);

            object sharedLock = new object();

            XssAttackingSharedReource sharedResource = new XssAttackingSharedReource(sharedQueue, sharedLock, _attack);

            for (int i = 0; i < _numberOfThreads; i++)
            {
                EncodedXssAttackerAgent agent = new EncodedXssAttackerAgent(sharedResource);
                agent.AgentAttackCompleted += agent_AgentAttackCompleted;
                agent.AgentAttackStarted += agent_AgentAttackStarted;
                agent.AgentAttackAnnounced += agent_AgentAttackAnnounced;
                agent.AttackAsync();
            }
        }

        #endregion
    }
}
