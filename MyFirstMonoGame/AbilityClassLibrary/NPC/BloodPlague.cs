using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class BloodPlague : Ability
    {
        public BloodPlague()
        {
            Name = "Blood Plague";
            Description = "Inflict light instant dmg and more dmg every turn for 3 turns.";
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = 6 + Convert.ToInt32(spellPower * .7);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }

        public int DoT(int spellPower, double crit, double multi, int increase)
        {
            var dmg = 3 + Convert.ToInt32(spellPower * .9);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
