using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class RigorMortis : Ability
    {
        public RigorMortis()
        {
            Name = "Rigor Mortis";
            Description = "Chill your enemy with pure death, dealing dmg and reducing their dmg dealt by 15% for 2 turns.";
        }

        public int Action(int spellPower, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(spellPower * 1.9);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
