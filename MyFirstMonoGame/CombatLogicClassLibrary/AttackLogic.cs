using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CombatLogicClassLibrary
{
    public static class AttackLogic
    {
        public static int CalculateAttackDamage(int baseDmg, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(baseDmg * multiplier) + increase;
            dmg = RandomProvider.GetRandom(Convert.ToInt32(dmg * .8), Convert.ToInt32(dmg * 1.2));
            if (isCrit(crit))
                return dmg * 2;
            else return dmg;
        }

        private static bool isCrit(double crit)
        {
            return crit >= RandomProvider.GetRandom(1, 100);
        }
    }
}
