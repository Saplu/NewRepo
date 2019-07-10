using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Shaman
{
    public class LightningBolt : Ability
    {
        public LightningBolt()
        {
            Name = "Lightning Bolt";
            Description = "Shoots a bolt\r\nof pure light \r\nto 1 enemy.";
            Cooldown = 1;
        }

        public int Action(int SpellPower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(SpellPower * 1.7) + 5;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
