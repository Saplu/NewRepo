using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.BloodPriest
{
    public class Heal : Ability
    {
        public Heal()
        {
            Name = "Heal";
            Description = "Heal 1 friend.";
            //Description = "Heals the target for sweet amount.";
            Cooldown = 2;
        }

        public int Action(int spellPower, double crit, double multi, int increase)
        {
            var heal = Convert.ToInt32(spellPower * 2.2);
            return AttackLogic.CalculateAttackDamage(heal, crit, multi, increase);
        }
    }
}
