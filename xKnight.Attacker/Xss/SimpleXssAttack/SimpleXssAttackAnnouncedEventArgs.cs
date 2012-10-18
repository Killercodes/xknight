using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{
    public class SimpleXssAttackAnnouncedEventArgs : AttackAnnouncedEventArgs
    {
        internal SimpleXssAttackAnnouncedEventArgs(SimpleXssAttackAnnounceItem announceItem) 
            :base (announceItem)
        {

        }
    }
}
