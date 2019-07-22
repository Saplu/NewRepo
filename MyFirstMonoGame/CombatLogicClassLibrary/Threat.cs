using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary
{
    [Serializable]
    public class Threat
    {
        private int[] threatTable;

        public int[] ThreatTable { get => threatTable; set => threatTable = value; }

        public Threat()
        {
            threatTable = new int[4] { 1, 1, 1, 1 };
        }

        public int[] ManageThreat(int index, int amount)
        {
            ThreatTable[index] += amount;
            
            return threatTable;
            
        }

        public int ChooseEnemy()
        {
            int max = 0;
            int index = 0;
            for (int i = 0; i < threatTable.Length; i++)
            {
                var current = threatTable[i];
                if (current > max)
                {
                    max = current;
                    index = i;
                }
            }
            return index + 1;
        }
    }
}
