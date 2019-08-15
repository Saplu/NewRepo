using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary.NPCClasses
{
    public class FinalRabbit : NPC, Interfaces.CombatInterface
    {
        int sweepUsed;

        public FinalRabbit(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multiplier = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multiplier * multiplier * (level * 235));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multiplier * (level * 11.5));
            Crit = 0;
            SpellPower = 0;
            Armor = Convert.ToInt32(multiplier * (level * 15));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
            sweepUsed = 4;
        }

        private int attack()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var attack = new AbilityClassLibrary.Warrior.Attack();
            sweepUsed++;
            return attack.Action(Strength, Crit, multi, increase);
        }

        private int sweep()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var sweep = new AbilityClassLibrary.Protector.Sweep();
            sweepUsed = 0;
            return sweep.Action(Strength - 30, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (sweepUsed == 4)
                return "Sweep";
            else return "Attack";
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Sweep": return 4;
                case "Attack": return 1;
                default: return 1;
            }
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Sweep": return sweep();
                case "Attack": return attack();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "Pupu";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            return list;
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            return 0;
        }
    }
}
