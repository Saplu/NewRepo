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
            threatTable = new int[4] { 25, 25, 25, 25 };
        }

        public int[] ManageThreat(int index, int amount)
        {
            if (amount != 0)
            {
                ThreatTable[index] += amount + Convert.ToInt32(amount / 3);
                ThreatTable[0] -= Convert.ToInt32(amount / 3);
                ThreatTable[1] -= Convert.ToInt32(amount / 3);
                ThreatTable[2] -= Convert.ToInt32(amount / 3);
                ThreatTable[3] -= Convert.ToInt32(amount / 3);
                ThreatTable[0] = inBounds(ThreatTable[0]);
                ThreatTable[1] = inBounds(ThreatTable[1]);
                ThreatTable[2] = inBounds(ThreatTable[2]);
                ThreatTable[3] = inBounds(ThreatTable[3]);
                makeIt100();
            }
            return threatTable;
        }

        public int ChooseEnemy()
        {
            var number = Utils.RandomProvider.GetRandom(1, 100);
            if (number <= threatTable[0])
                return 1;
            else if (number > threatTable[0] && number <= threatTable[0] + threatTable[1])
                return 2;
            else if (number > threatTable[0] + threatTable[1] && number <= threatTable[0] + threatTable[1] + threatTable[2])
                return 3;
            else return 4;
        }

        private int inBounds(int final)
        {
            if (final >= 0 && final <= 100)
                return final;
            else if (final < 0)
                return 0;
            else return 100;
        }

        private void makeIt100()
        {
            var sum = ThreatTable.Sum();
            if (sum != 100)
            {
                if (ThreatTable[0] != 0)
                    ThreatTable[0] += 100 - sum;
                else if (ThreatTable[1] != 0)
                    ThreatTable[1] += 100 - sum;
                else if (ThreatTable[2] != 0)
                    ThreatTable[2] += 100 - sum;
                else ThreatTable[3] += 100 - sum;
            }
        }
    }
}
