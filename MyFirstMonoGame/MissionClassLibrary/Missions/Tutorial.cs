using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class Tutorial : Mission
    {
        public Tutorial(List<Player> players)
        {
            var enemy = new CharacterClassLibrary.NPCClasses.Rabbit(1, 1);
            enemy.Position = 5;
            Enemies = new List<NPC>() { enemy };
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
