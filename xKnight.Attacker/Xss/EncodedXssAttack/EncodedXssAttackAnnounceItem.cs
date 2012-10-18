using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xKnight.Models;

namespace xKnight.Attacking
{
    public class EncodedXssAttackAnnounceItem : AttackAnnounceItem
    {
        public XAttack XAttack { get; private set; }
        public EncodedXssAttackStatus AttackStatus { get; private set; }

        public EncodedXssAttackAnnounceItem(XAttack xAttack, EncodedXssAttackStatus attackStatus, XssAttackingSharedReource attackingSharedResource, string description, DateTime dateTime)
            :base(attackingSharedResource,description,dateTime)
        {
            this.XAttack = xAttack;
            this.AttackStatus = attackStatus;
        }
    }
}
