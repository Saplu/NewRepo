using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary
{
    public class RandomEnemyGenerator
    {
        public NPC CreateEnemy(Enums.NPCType type, int level)
        {
            var random = Utils.RandomProvider.GetRandom(0, 4);
            var className = (Enums.NPCClassName)Enum.Parse(typeof(Enums.NPCClassName), random.ToString());
            return NPC.Create(className, type, level);
        }
        
    }
}
