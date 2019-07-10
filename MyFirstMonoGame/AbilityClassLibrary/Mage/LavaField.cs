using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Mage
{
    public class LavaField : Ability
    {
        public LavaField()
        {
            Name = "Lava Field";
            Description = "Attack 3\r\nenemies instantly\r\nand at the end\r\nof turn.";
            Cooldown = 3;
        }

        public int Action(int spellpower, double crit, double multiplier, int increase)
        {
            var dmg = 5 + spellpower;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
