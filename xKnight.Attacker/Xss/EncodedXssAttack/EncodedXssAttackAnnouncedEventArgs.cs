using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{
    public class EncodedXssAttackAnnouncedEventArgs : AttackAnnouncedEventArgs
    {
        internal EncodedXssAttackAnnouncedEventArgs(EncodedXssAttackAnnounceItem announceItem) 
            :base (announceItem)
        {

        }
    }
}
