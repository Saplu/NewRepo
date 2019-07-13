using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Mage
{
    public class FireWithin : Ability
    {
        public FireWithin()
        {
            Name = "Fire Within";
            Description = "Hit 1 enemy\r\nfor true dmg.";
            Cooldown = 3;
        }

        public int Action(int spellpower, double crit, double multiplier, int increase)
        {
            var dmg = 10 + Convert.ToInt32(spellpower * 1.7);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
