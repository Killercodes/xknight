using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using xKnight.Models;

namespace xKnight.Attacking
{
    public class XssAttackingSharedReource : AttackingSharedReource
    {
        internal Queue<Form> SharedQueue { get; private set; }

        public XssAttackingSharedReource(Queue<Form> sharedQueue, object sharedLock,Attack sharedAttack)
            : base(sharedLock, sharedAttack)
        {
            this.SharedQueue = sharedQueue;
        }
    }
}
