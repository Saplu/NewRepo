using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Rogue
{
    public class Jawbreaker : Ability
    {
        public Jawbreaker()
        {
            Name = "Jawbreaker";
            Description = "Cost: 50 energy.\r\nStun 1 enemy\r\nfor 1 turn.\r\nApply poison and\r\ngrant 1 \r\ncombo point.";
            Cooldown = 4;
        }

        public int Action(int Strength, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(Strength * .8);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
