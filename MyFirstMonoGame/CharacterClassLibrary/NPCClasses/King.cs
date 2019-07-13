using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class King : NPC, Interfaces.CombatInterface
    {
        public King(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (130 + (102 * level)));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multi * (16 + (level * 7)));
            Crit = 0;
            SpellPower = 0;
            Armor = Convert.ToInt32(multi * (level * 13));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
        }

        private int attack()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var attack = new AbilityClassLibrary.Warrior.Attack();
            Statuses.Add(new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(100, new List<int>(), 1.02));
            return attack.Action(Strength, Crit, multi, increase);
        }

        private int stunningBlow()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var stun = new AbilityClassLibrary.NPC.StunningBlow();
            Statuses.Add(new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(100, new List<int>(), 1.02));
            return stun.Action(Strength, Crit, multi, increase);
        }

        private int whirlwind()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var whirl = new AbilityClassLibrary.NPC.Whirlwind();
            Statuses.Add(new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(100, new List<int>(), 1.02));
            return whirl.Action(Strength, Crit, multi, increase);
        }

        private int sweep()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var sweep = new AbilityClassLibrary.Protector.Sweep();
            Statuses.Add(new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(100, new List<int>(), 1.02));
            return sweep.Action(Strength - 20, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            var rand = RandomProvider.GetRandom(1, 100);
            if (rand > 80)
                return "Stunning Blow";
            else if (rand > 60)
                return "Whirlwind";
            else if (rand > 40)
                return "Sweep";
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
                case "Attack": return attack();
                case "Whirlwind": return whirlwind();
                case "Stunning Blow": return stunningBlow();
                case "Sweep": return sweep();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "kunkku";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();
            switch (id)
            {
                case "Stunning Blow": list = util.setTargets(targetPosition, 1); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch(id)
            {
                case "Stunning Blow": return 2;
                default: return 0;
            }
        }
    }
}
