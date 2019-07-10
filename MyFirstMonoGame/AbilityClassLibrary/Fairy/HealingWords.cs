using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Fairy
{
    public class HealingWords : Ability
    {
        public HealingWords()
        {
            Name = "Healing Words";
            Description = "Heal 1 target \r\ninstantly and \r\neach turn \r\nfor 3 turns.";
            Cooldown = 2;
        }

        public int Action(int SpellPower, double crit, double multi, int increase)
        {
            return AttackLogic.CalculateAttackDamage(SpellPower, crit, multi, increase);
        }
    }
}
