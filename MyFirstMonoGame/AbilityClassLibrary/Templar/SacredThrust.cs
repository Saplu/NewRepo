using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Templar
{
    public class SacredThrust : Ability
    {
        public SacredThrust()
        {
            Name = "Sacred Thrust";
            Description = "Attack 1 enemy\r\nfor high threat.";
            Cooldown = 1;
        }

        public int Action(int SpellPower, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(SpellPower * 1.3);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
