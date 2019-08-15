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
    public class Necromancer : NPC, Interfaces.CombatInterface
    {
        public Necromancer(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (60 + (47 * level)));
            MaxHealth = Health;
            Strength = 0;
            Crit = 10;
            SpellPower = Convert.ToInt32(multi * (12 + (9.5*level)));
            Armor = Convert.ToInt32(multi * (level * 6));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int bloodPlague()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var plague = new BloodPlague();
            return plague.Action(SpellPower, Crit, multi, increase);
        }

        private int rigorMortis()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var rigor = new RigorMortis();
            return rigor.Action(SpellPower, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 70)
                return "RigorMortis";
            else return "BloodPlague";
        }

        public override int GetTargets(string id)
        {
            return 1;
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "RigorMortis": return rigorMortis();
                case "BloodPlague": return bloodPlague();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "necro";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();

            switch(id)
            {
                case "RigorMortis": list = util.setTargets(targetPosition, 1); return list;
                case "BloodPlague": list = util.setTargets(targetPosition, 1); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch(id)
            {
                case "RigorMortis": return .85;
                case "BloodPlague":
                    var multi = getAttackMultiplier();
                    var increase = getAttackModifier();
                    var plague = new BloodPlague();
                    return plague.DoT(SpellPower, Crit, multi, increase);
                default: return 0;
            }
        }
    }
}
