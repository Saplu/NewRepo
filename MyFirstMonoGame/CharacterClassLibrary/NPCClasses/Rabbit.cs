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
    public class Rabbit : NPC, Interfaces.CombatInterface
    {
        public Rabbit(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multiplier = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multiplier * multiplier * (111 + (level * 28)));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multiplier * (9 + (level * 3.5)));
            Crit = 10;
            SpellPower = 0;
            Armor = Convert.ToInt32(multiplier * (level * 15));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int kick()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var kick = new Kick();
            return kick.Action(Strength, Crit, multi, increase);
        }

        private int bite()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var bite = new Bite();
            return bite.Action(Strength, Crit, multi, increase);
        }
        
        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 50)
                return "Kick";
            else return "Bite";
        }

        public override int GetTargets(string id)
        {
            if (id == "Kick")
                return 1;
            else if (id == "Bite")
                return 1;
            else return 1;
        }

        public override int UseAbility(string id)
        {
            if (id == "Kick")
                return kick();
            else if (id == "Bite")
                return bite();
            else return 1;
        }

        public override string setPic()
        {
            return "Pasi";
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
