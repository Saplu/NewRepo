using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class Outpost : Mission
    {
        public Outpost(List<Player> players)
        {
            var Enemy1 = new CharacterClassLibrary.NPCClasses.Pirate(4, 2);
            var Enemy2 = new CharacterClassLibrary.NPCClasses.Goblin(5, 1);
            var Enemy3 = new CharacterClassLibrary.NPCClasses.Necromancer(4, 1);
            var Enemy4 = new CharacterClassLibrary.NPCClasses.Necromancer(4, 3);
            Enemies = new List<NPC>() { Enemy1, Enemy2, Enemy3, Enemy4 };
            foreach (var enemy in Enemies)
            {
                var numb = Enemies.IndexOf(enemy);
                enemy.Position = numb + 5;
            }
            Players = new List<Player>();
            foreach (var player in players)
                Players.Add(player);
            foreach (var player in Players)
            {
                var numb = Players.IndexOf(player);
                player.Position = numb + 1;
            }
            Turn = 1;
            Level = 4;
            ActionsTaken = new List<int>();
            RewardTable = new int[4] { 50, 50, 0, 0 };
            TransferTo = "Rampart";
        }
    }
}
