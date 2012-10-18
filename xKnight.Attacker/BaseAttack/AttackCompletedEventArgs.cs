using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{

    public class AttackCompletedEventArgs
    {
        public Attacker Attacker{get;private set;} 

        internal AttackCompletedEventArgs(Attacker attacker)
        {
            Attacker=attacker;
        }        
    }

}
