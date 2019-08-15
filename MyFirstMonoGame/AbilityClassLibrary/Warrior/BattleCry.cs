using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Warrior
{
    public class BattleCry : Ability
    {
        public BattleCry()
        {
            Name = "Battle Cry";
            Description = "Light dmg on \r\nevery enemy.\r\nIncrease dmg\r\nof party by\r\n15% for \r\ntwo turns.";
            Cooldown = 2;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength / 2);            
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
