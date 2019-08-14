using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.NPC;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    public class Brute : NPC, Interfaces.CombatInterface
    {
        int swipeUsed;

        public Brute(int level, int type)
        {
            Type = (Enums.NPCType)Enum.Parse(typeof(Enums.NPCType), type.ToString());
            var multi = typeMultiplier();
            Level = level;
            Health = Convert.ToInt32(multi * multi * (200 * level));
            MaxHealth = Health;
            Strength = Convert.ToInt32(multi * (13 * level));
            Crit = 0;
            SpellPower = 0;
            Armor = Convert.ToInt32(multi * (level * 15));
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Threat = new CombatLogicClassLibrary.Threat();
            swipeUsed = 0;
        }

        private int execute()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var exe = new Execute();
            RecieveHeal(200);
            Statuses.Add(new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(100, new List<int>(), 1.04));
            swipeUsed++;
            return exe.Action(Strength, Crit, multi, increase);
        }

        private int sweep()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var sweep = new AbilityClassLibrary.Protector.Sweep();
            RecieveHeal(200);
            Statuses.Add(new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(100, new List<int>(), 1.04));
            swipeUsed = 0;
            return sweep.Action(Strength - 50, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (swipeUsed == 4)
                return "Sweep";
            else return "Execute";
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Sweep": return 4;
                case "Execute": return 1;
                default: return 1;
            }
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Sweep": return sweep();
                case "Execute": return execute();
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "Brute";
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
