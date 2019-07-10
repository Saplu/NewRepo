using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class HoT : Status
    {
        public HoT(int duration, List<int> targets, int heal)
        {
            Name = StatusEnums.HoT;
            Duration = duration;
            Targets = targets;
            Effect = heal;
        }

        public override string ToString()
        {
            return "\r\nHoT rolling";
        }
    }
}
