using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    public class CastleSecond : Mission
    {
        public CastleSecond(List<Player> players)
        {
            var Enemy1 = new CharacterClassLibrary.NPCClasses.Brute(9, 3);
            Enemies = new List<NPC>() { Enemy1 };
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
            Level = 8;
            RewardTable = new int[4] { 0, 0, 0, 100 };
            Logger = new Logger();
        }
    }
}
