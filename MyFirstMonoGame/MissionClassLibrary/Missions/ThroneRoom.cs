using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class ThroneRoom : Mission
    {
        public ThroneRoom(List<Player> players)
        {
            var Enemy1 = new CharacterClassLibrary.NPCClasses.King(7, 3);
            var Enemy2 = new CharacterClassLibrary.NPCClasses.Medic(6, 2);
            Enemies = new List<NPC>() { Enemy1, Enemy2};
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
            Level = 6;
            ActionsTaken = new List<int>();
            RewardTable = new int[4] { 0, 0, 80, 20 };
            TransferTo = "Menu";
        }
    }
}
