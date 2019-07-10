using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.BloodPriest
{
    public class LifeLeech : Ability
    {
        public LifeLeech()
        {
            Name = "Life Leech";
            Description = "Attack 1 enemy.\r\nsmall HoT for \r\nevery friend \r\nfor 1 turn";
            //Description = "Deal damage to a single enemy. Every party member gains a HoT for a part of damage dealt for 1 turn.";
            Cooldown = 1;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellPower * 1.4);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
