﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class PoisonExplosion : Ability
    {
        public PoisonExplosion()
        {
            Name = "Poison Explosion";
            Description = "";
        }

        public int Action(int spellPower, double crit, double multi, int increase)
        {
            var dmg = spellPower;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
