using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class Sweep : Ability
    {
        public Sweep()
        {
            Name = "Sweep";
            Description = "Hit all enemies\r\nfor low dmg\r\nand extra threat.";
            Cooldown = 0;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.1);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
