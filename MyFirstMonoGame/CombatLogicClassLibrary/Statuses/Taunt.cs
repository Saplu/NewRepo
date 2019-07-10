using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class Taunt : Status
    {
        public Taunt(int duration, List<int> targets, int position)
        {
            Name = StatusEnums.Taunt;
            Duration = duration;
            Targets = targets;
            Effect = position;
        }

        public override string ToString()
        {
            return "\r\nTaunted";
        }
    }
}
