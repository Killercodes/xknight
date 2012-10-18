using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.Attacking
{
    public class SimpleXssAttackAnnounceItem : AttackAnnounceItem
    {
        public XAttack XAttack { get; private set; }
        public SimpleXssAttackStatus AttackStatus { get; private set; }

        public SimpleXssAttackAnnounceItem(XAttack xAttack, SimpleXssAttackStatus attackStatus, XssAttackingSharedReource attackingSharedResource, string description, DateTime dateTime)
            :base(attackingSharedResource,description,dateTime)
        {
            this.XAttack = xAttack;
            this.AttackStatus = attackStatus;
        }
    }
}
