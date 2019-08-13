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
            Description = "Attack 1 enemy\r\nand reduce the\r\ndmg it deals\r\nby 10% for 2 turns";
            Cooldown = 1;
        }

        public int Action(int spellpower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellpower * 1.5);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
