using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Fairy
{
    public class Bubble : Ability
    {
        public Bubble()
        {
            Name = "Bubble";
            Description = "Heal the target \r\nand absorb \r\nsame amount of \r\ndamage for \r\nmaximum of \r\n3 turns.";
            Cooldown = 2;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var value = Convert.ToInt32(spellPower * 1.4);
            return AttackLogic.CalculateAttackDamage(value, crit, multiplier, increase);
        }
    }
}
