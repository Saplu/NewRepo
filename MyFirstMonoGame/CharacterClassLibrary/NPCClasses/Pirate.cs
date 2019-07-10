using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Warrior;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class Pirate : NPC, Interfaces.CombatInterface
    {
        public Pirate(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (107 + (51 * level)));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multi * (9 + (level * 3.5)));
            Crit = 10;
            SpellPower = 0;
            Armor = Convert.ToInt32(multi * (level * 6));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int attack()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var attack = new Attack();
            return attack.Action(Strength, Crit, multi, increase);
        }

        private int viciousBlow()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var vicious = new ViciousBlow();
            return vicious.Action(Strength, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 75)
                return "Vicious Blow";
            else return "Attack";
        }

        public override int GetTargets(string id)
        {
            return 1;
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Attack": return attack();
                case "Vicious Blow": return viciousBlow();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "merkkari";
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
