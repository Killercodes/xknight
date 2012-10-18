using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{
    public class AttackAnnouncedEventArgs
    {
        public AttackAnnounceItem AnnounceItem { get; private set; }

        internal AttackAnnouncedEventArgs(AttackAnnounceItem announceItem)
        {
            AnnounceItem = announceItem;
        }
    }
}
