using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Warrior
{
    public class Attack : Ability
    {
        public Attack()
        {
            Name = "Attack";
            Description = "Attack 1 enemy";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = strength * 2;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
