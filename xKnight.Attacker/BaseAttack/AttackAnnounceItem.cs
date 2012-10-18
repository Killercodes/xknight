using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xKnight.Attacking
{
    public class AttackAnnounceItem
    {
        public string Description { get; private set; }
        public DateTime DateTime { get; private set; }
        public AttackingSharedReource AttackingSharedResource { get; private set; }

        public AttackAnnounceItem(AttackingSharedReource attackingSharedResource, string description, DateTime dateTime)
        {
            this.AttackingSharedResource = attackingSharedResource;
            this.Description = description;
            this.DateTime = dateTime;
        }
    }
}
