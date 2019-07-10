using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Warrior
{
    public class Execute : Ability
    {
        public Execute()
        {
            Name = "Execute";
            Description = "Insane dmg on\r\n1 enemy.\r\nHigh crit chance.";
            Cooldown = 5;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = 5 + strength * 4;
            crit = crit + 25;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
