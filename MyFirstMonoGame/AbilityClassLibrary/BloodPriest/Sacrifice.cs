using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.BloodPriest
{
    public class Sacrifice : Ability
    {
        public Sacrifice()
        {
            Name = "Sacrifice";
            Description = "Lose 10% of hp\r\nto heal 1 friend.\r\nReduce dmg \r\ntaken by target\r\n for 3 turns.";
            //Description = "Sacrifice 10% of your health to heal your friend for a great amount " +
                //"and give him a shield that reduces damage he takes for 3 turns.";
            Cooldown = 5;
        }

        public int Action(int sp, double crit, double multiplier, int increase)
        {
            var heal = sp * 3;
            return AttackLogic.CalculateAttackDamage(heal, crit, multiplier, increase);
        }
    }
}
