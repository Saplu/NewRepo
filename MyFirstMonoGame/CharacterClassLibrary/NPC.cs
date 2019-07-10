using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;
using CombatLogicClassLibrary;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class NPC : Character
    {
        private Threat threat;
        private Enums.NPCType type;

        public Threat Threat { get => threat; set => threat = value; }
        public NPCType Type { get => type; set => type = value; }

        public abstract string ChooseAbility();

        public static NPC Create(NPCClassName classname, NPCType type, int level)
        {
            switch(classname)
            {
                case NPCClassName.Rabbit: return new NPCClasses.Rabbit(level, Convert.ToInt32(type));
                case NPCClassName.Goblin: return new NPCClasses.Goblin(level, Convert.ToInt32(type));
                case NPCClassName.Pirate: return new NPCClasses.Pirate(level, Convert.ToInt32(type));
                case NPCClassName.Necromancer: return new NPCClasses.Necromancer(level, Convert.ToInt32(type));
                case NPCClassName.Medic: return new NPCClasses.Medic(level, Convert.ToInt32(type));
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public int ChooseEnemy()
        {
            foreach (var status in Statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.Taunt)
                    return Convert.ToInt32(status.Effect);
            }
            return Threat.ChooseEnemy();
        }

        public void ManageThreat(int index, int amount)
        {
            Threat.ManageThreat(index, amount);
        }

        public void ManageThreat(int index)
        {
            Threat.ManageThreat(index, -Threat.ThreatTable[index]);
        }

        public int ChooseAlly(double[] percents)
        {
            var lowestIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                var lowest = percents.Min();
                lowestIndex = Array.IndexOf(percents, percents.Min());
                if (lowest > 0)
                    break;
                else
                {
                    percents[lowestIndex] += 1;
                }
            }
            return lowestIndex;
        }

        public void RemoveTaunt()
        {
            var keeplist = new List<Status>();
            foreach (var status in Statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.Taunt) ;
                else keeplist.Add(status);
            }
            Statuses = keeplist;
        }

        protected double typeMultiplier()
        {
            switch (Type)
            {
                case Enums.NPCType.Recruit: return .7;
                case Enums.NPCType.Normal: return 1;
                case Enums.NPCType.Veteran: return 1.3;
                case Enums.NPCType.Elite: return 1.7;
                default: return 1;
            }
        }
    }
}
