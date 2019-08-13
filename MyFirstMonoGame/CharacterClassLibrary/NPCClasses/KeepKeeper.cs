using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.NPC;
using AbilityClassLibrary.Protector;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class KeepKeeper : NPC, Interfaces.CombatInterface
    {
        public KeepKeeper(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (100 + (75 * level)));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multi * (8 + (level * 6.5)));
            Crit = 10;
            SpellPower = 0;
            Armor = Convert.ToInt32(multi * (level * 13));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int whirlwind()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var whirl = new Whirlwind();
            return whirl.Action(Strength - 5, Crit, multi, increase);
        }

        private int sweep()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var sweep = new Sweep();
            return sweep.Action(Strength - 15, 0, multi, increase);
        }

        private int attack()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var attack = new AbilityClassLibrary.Warrior.Attack();
            return attack.Action(Strength, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            var rand = RandomProvider.GetRandom(1, 100);
            if (rand > 80) return "Whirlwind";
            else if (rand > 60) return "Sweep";
            else return "Attack";
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Whirlwind": return 2;
                case "Sweep": return 4;
                default: return 1;
            }
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Whirlwind": return whirlwind();
                case "Sweep": return sweep();
                case "Attack": return attack();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "keeper";
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
