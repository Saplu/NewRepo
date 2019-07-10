using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class Shield : Status
    {
        public Shield(int duration, List<int> targets, int value)
        {
            Name = StatusEnums.Shield;
            Duration = duration;
            Targets = targets;
            Effect = value;
        }

        public override string ToString()
        {
            return "\r\nShielded";
        }
    }
}
