using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class AttackDmgMultiplier : Status
    {
        public AttackDmgMultiplier(int duration, List<int> targets, double multiplier)
        {
            Name = StatusEnums.AttackDmgMultiplier;
            Duration = duration;
            Targets = targets;
            Effect = multiplier;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
