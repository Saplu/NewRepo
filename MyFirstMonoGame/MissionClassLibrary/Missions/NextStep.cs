using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class NextStep : Mission
    {
        public NextStep(List<Player> players)
        {
            var enemy1 = new CharacterClassLibrary.NPCClasses.Rabbit(1, 1);
            enemy1.Position = 5;
            var enemy2 = new CharacterClassLibrary.NPCClasses.Rabbit(1, 1);
            enemy2.Position = 6;
            Enemies = new List<NPC>() { enemy1, enemy2 };
            Players = new List<Player>();
            foreach (var player in players)
                Players.Add(player);
            foreach (var player in Players)
            {
                var numb = Players.IndexOf(player);
                player.Position = numb + 1;
            }
            Turn = 1;
            Level = 1;
            RewardTable = new int[4] { 100, 0, 0, 0 };
            TransferTo = "Menu";
        }
    }
}
