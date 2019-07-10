using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class ChallengingShout : Ability
    {
        public ChallengingShout()
        {
            Name = "Challenging Shout";
            Description = "Taunt every enemy\r\nand take 75%\r\nless dmg\r\nfor 1 turn";
            Cooldown = 5;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * .5);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }

        public Status applySelfStatus()
        {
            var status = new CombatLogicClassLibrary.Statuses.TakenDmgMultiplier(1, new List<int>(), .75);
            return status;
        }
    }
}
