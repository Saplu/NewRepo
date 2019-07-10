using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Shaman
{
    public class FlameShock : Ability
    {
        public FlameShock()
        {
            Name = "Flame Shock";
            Description = "Ignites the\r\ntarget on fire,\r\ndealing damage\r\nfor 3 turns.";
            Cooldown = 3;
        }

        public int Action(int SpellPower, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(SpellPower * 1.5);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
