using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class TauntingBlow : Ability
    {
        public TauntingBlow()
        {
            Name = "Taunting Blow";
            Description = "Taunt 1 enemy\r\nfor 2 turns.\r\nAlso low dmg.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            return AttackLogic.CalculateAttackDamage(strength, crit, multiplier, increase);
        }
    }
}
