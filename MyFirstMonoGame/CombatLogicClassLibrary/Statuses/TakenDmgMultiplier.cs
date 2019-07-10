using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class TakenDmgMultiplier : Status
    {
        public TakenDmgMultiplier(int duration, List<int> targets, double effect)
        {
            Name = StatusEnums.TakenDmgMultiplier;
            Duration = duration;
            Targets = targets;
            Effect = effect;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
