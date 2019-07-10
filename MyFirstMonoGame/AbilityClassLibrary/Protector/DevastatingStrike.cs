using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class DevastatingStrike : Ability
    {
        public DevastatingStrike()
        {
            Name = "Devastating Strike";
            Description = "Attack 1 enemy\r\nfor low dmg\r\nand high threat";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.7);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
