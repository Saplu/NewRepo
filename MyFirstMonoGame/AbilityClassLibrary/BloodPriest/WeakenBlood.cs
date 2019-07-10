using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.BloodPriest
{
    public class WeakenBlood : Ability
    {
        public WeakenBlood()
        {
            Name = "Weaken Blood";
            Description = "Dmg to 1 enemy\r\nmake target \r\nvulnerable for\r\nthis turn.";
            //Description = "Deal light damage and make target vulnerable for all damage for this turn.";
            Cooldown = 3;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            return AttackLogic.CalculateAttackDamage(spellPower, crit, multiplier, increase);
        }
    }
}
