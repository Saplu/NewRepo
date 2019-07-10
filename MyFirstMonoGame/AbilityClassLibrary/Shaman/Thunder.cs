using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Shaman
{
    public class Thunder : Ability
    {
        public Thunder()
        {
            Name = "Thunder";
            Description = "Attack every\r\nenemy and stun\r\nprimary target\r\nfor 1 turn.";
            Cooldown = 5;
        }

        public int Action(int SpellPower, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(SpellPower * 1.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
