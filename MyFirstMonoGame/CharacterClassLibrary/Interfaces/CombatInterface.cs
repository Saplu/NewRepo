using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary.Interfaces
{
    public interface CombatInterface
    {
        void Defend(int dmg, int attackerLevel);
        void TrueDmgDefend(int dmg);
    }
}
