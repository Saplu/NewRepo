using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityClassLibrary.NPC
{
    public class StunningBlow : Ability
    {
        public StunningBlow()
        {
            Name = "Stunning Blow";
            Description = "Deals damage and stuns the target for 1 turn.";
        }

        public int Action(int strength,double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.3);
            return CombatLogicClassLibrary.AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
