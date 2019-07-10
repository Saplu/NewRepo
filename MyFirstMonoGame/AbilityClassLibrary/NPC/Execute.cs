using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class Execute : Ability
    {
        public Execute()
        {
            Name = "Execute";
            Description = "Hit your enemy with real intention of killing it.";
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = 10 + strength * 2;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
