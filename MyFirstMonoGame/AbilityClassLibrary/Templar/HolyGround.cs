using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Templar
{
    public class HolyGround : Ability
    {
        public HolyGround()
        {
            Name = "Holy Ground";
            Description = "Deals damage to\r\nall enemies for\r\n3 turns and \r\ninstant threat.";
            Cooldown = 3;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellPower * .8);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
