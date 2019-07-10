using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class Stun : Status
    {
        public Stun(int duration, List<int>targets)
        {
            Name = StatusEnums.Stun;
            Duration = duration;
            Targets = targets;
        }

        public override string ToString()
        {
            return "\r\nStunned";
        }
    }
}
