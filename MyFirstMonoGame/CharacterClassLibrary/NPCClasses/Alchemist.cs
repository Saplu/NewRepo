using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.NPC;
using Utils;
using AbilityClassLibrary.Mage;

namespace CharacterClassLibrary.NPCClasses
{
    public class Alchemist : NPC, Interfaces.CombatInterface
    {
        public Alchemist(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (80 + (48 * level)));
            MaxHealth = Health;
            Strength = 0;
            Crit = 10;
            SpellPower = Convert.ToInt32(multi * (12 + (9 * level)));
            Armor = Convert.ToInt32(multi * (level * 9));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int fireball()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var leech = new Fireball();
            RecieveHeal(Convert.ToInt32(SpellPower * .3));
            return leech.Action(SpellPower, Crit, multi, increase);
        }

        private int poisonExplosion()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var poison = new PoisonExplosion();
            return poison.Action(SpellPower, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 40)
                return "Fireball";
            else return "Poison Explosion";
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Fireball": return 1;
                case "Poison Explosion": return 4;
                default: return 1;
            }
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Fireball": return fireball();
                case "Poison Explosion": return poisonExplosion();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "BloodPriest";
        }

        public override List<int> setStatusTargets(string id,int targetPosition, int enemyCount)
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
