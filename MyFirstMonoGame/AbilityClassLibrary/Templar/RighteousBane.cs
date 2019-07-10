using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Templar
{
    public class RighteousBane : Ability
    {
        public RighteousBane()
        {
            Name = "Righteous Bane";
            Description = "Attack 1 target\r\nand reduce dmg\r\nit deals by\r\n50% for \r\n2 turns.";
            Cooldown = 5;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = spellPower * 3;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
