using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{[Serializable]
    public class TakenDmgModifier : Status
    {
        public TakenDmgModifier(int duration, List<int> targets, int modifier)
        {
            Name = StatusEnums.TakenDmgModifier;
            Duration = duration;
            Targets = targets;
            Effect = modifier;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
