using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using xKnight.Models;

namespace xKnight.Attacking
{
    public abstract class AttackingSharedReource
    {
        
        #region Fields

        private int _numberOfAttacks = 0;
        
        #endregion

        #region Propertiens

        internal object SharedLock { get; private set; }
        internal Attack SharedAttack { get; private set; }
        
        public int NumberOfAttacks{get{return _numberOfAttacks;}}

#endregion

        public AttackingSharedReource(object sharedLock,Attack sharedAttack)
        {
            this.SharedAttack = sharedAttack;
            this.SharedLock = sharedLock;
        }

        internal void IncrementAttacks()
        {
            Interlocked.Increment(ref _numberOfAttacks);
        }
    }
}
