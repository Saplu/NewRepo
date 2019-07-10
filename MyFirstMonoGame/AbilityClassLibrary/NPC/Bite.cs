using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class Bite : Ability
    {
        public Bite()
        {
            Name = "Bite";
            Description = "Bites off a chunk of his enemys flesh. Tiny bit.";
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(3 + strength*1.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
