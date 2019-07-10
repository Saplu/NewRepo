using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Rogue
{
    public class BladeFlurry : Ability
    {
        public BladeFlurry()
        {
            Name = "Blade Flurry";
            Description = "Cost: 40 energy\r\nand all combo\r\npoints. Deal\r\ndmg for every\r\npoint consumed.\r\nApplies poison.\r\n" +
                "With 5 points\r\npoison every enemy.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multi, int increase, int combo)
        {
            var dmg = Convert.ToInt32(strength * combo * .8);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
