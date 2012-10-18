using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{
    public class AttackStartedEventArgs
    {
        public Attacker Attacker { get; private set; }

        internal AttackStartedEventArgs(Attacker attacker)
        {
            Attacker = attacker;
        }
    }
}
