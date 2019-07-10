using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Rogue
{
    public class Mutilate : Ability
    {
        public Mutilate()
        {
            Name = "Mutilate";
            Description = "Cost: 60 energy.\r\nAttack 1 enemy,\r\napply two stacks\r\nof poison and\r\ngain two combo\r\npoints.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * 2.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
