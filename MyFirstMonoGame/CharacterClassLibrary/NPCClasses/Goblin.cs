using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.NPC;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class Goblin : NPC, Interfaces.CombatInterface
    {
        public Goblin(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (106 + (43 * level)));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multi * (5 + (level * 7)));
            Crit = 10;
            SpellPower = 0;
            Armor = Convert.ToInt32(multi * (level * 12));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int whirlwind()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var whirl = new Whirlwind();
            return whirl.Action(Strength, Crit, multi, increase);
        }

        private int execute()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var exe = new Execute();
            return exe.Action(Strength, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 70)
                return "Whirlwind";
            else return "Execute";
        }

        public override int GetTargets(string id)
        {
            if (id == "Whirlwind")
                return 2;
            else if (id == "Execute")
                return 1;
            else return 1;
        }

        public override int UseAbility(string id)
        {
            if (id == "Whirlwind")
                return whirlwind();
            else if (id == "Execute")
                return execute();
            else return 1;
        }

        public override string setPic()
        {
            return "Goblin";
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
