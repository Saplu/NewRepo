using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class Poison : Status
    {
        public Poison(int duration, List<int> targets, int dmg)
        {
            Name = StatusEnums.Poison;
            Duration = duration;
            Targets = targets;
            Effect = dmg;
        }

        public override string ToString()
        {
            return "\r\nPoisoned";
        }
    }
}
