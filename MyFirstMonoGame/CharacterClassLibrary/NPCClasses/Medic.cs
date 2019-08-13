using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.BloodPriest;
using AbilityClassLibrary.Fairy;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class Medic: NPC, Interfaces.CombatInterface
    {
        public Medic(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (83 + (40 * level)));
            MaxHealth = Health;
            Strength = 0;
            Crit = 10;
            SpellPower = Convert.ToInt32(multi * (6 + level * 6));
            Armor = Convert.ToInt32(multi * (level * 9));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int heal()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var heal = new Heal();
            return heal.Action(SpellPower, Crit, multi, increase);
        }

        private int healingWords()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var heal = new HealingWords();
            return heal.Action(SpellPower, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 70)
                return "Healing Words";
            else return "Heal";
        }

        public override int GetTargets(string id)
        {
            return 1;
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Heal": return heal();
                case "Healing Words": return healingWords();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "medic";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();
            switch(id)
            {
                case "Healing Words": list = util.setTargets(targetPosition, 1); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch (id)
            {
                case "Healing Words": return healingWords();
                default: return 0;
            }
        }
    }
}
