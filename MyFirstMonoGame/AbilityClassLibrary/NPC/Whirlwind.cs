using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class Whirlwind : Ability
    {
        public Whirlwind()
        {
            Name = "Whirlwind";
            Description = "Hits two enemies at a time. Not really like a whirling wind, but close enough.";
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = 4 + Convert.ToInt32(strength * 1.3);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
