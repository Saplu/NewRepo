using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Fairy
{
    public class Laser : Ability
    {
        public Laser()
        {
            Name = "Laser";
            Description = "Shoots pink laser from her eyes to an enemy, reducing damage the target deals by 10% for 2 turns.";
            Cooldown = 1;
        }

        public int Action(int spellpower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellpower * 1.5);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
