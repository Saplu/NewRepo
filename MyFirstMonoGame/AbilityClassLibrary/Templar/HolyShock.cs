using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Templar
{
    public class HolyShock : Ability
    {
        public HolyShock()
        {
            Name = "Holy Shock";
            Description = "Hurt 3 enemies.\r\nTaunt primary\r\ntarget for \r\n1 turn.";
            Cooldown = 3;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellPower * 1.1);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
