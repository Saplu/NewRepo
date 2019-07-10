using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary.Statuses
{
    [Serializable]
    public class AttackDmgModifier : Status
    {
        public AttackDmgModifier(int duration, List<int> targets, int effect)
        {
            Name = StatusEnums.AttackDmgModifier;
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
