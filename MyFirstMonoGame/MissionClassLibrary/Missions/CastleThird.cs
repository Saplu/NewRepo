using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    public class CastleThird : Mission
    {
        public CastleThird(List<Player> players)
        {
            var Enemy1 = new CharacterClassLibrary.NPCClasses.FinalRabbit(10, 3);
            var Enemy2 = new CharacterClassLibrary.NPCClasses.Rabbit(9, 3, 1);
            Enemy2.Health = 10000;
            var Enemy3 = new CharacterClassLibrary.NPCClasses.Rabbit(9, 3, 1);
            Enemy3.Health = 1000;
            Enemies = new List<NPC>() { Enemy1, Enemy2, Enemy3 };
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
            Level = 9;
            RewardTable = new int[4] { 0, 0, 0, 100 };
            Logger = new Logger();
        }

        public override bool CheckWin()
        {
            if (Enemies[0].Health <= 0)
                return true;
            else return false;
        }
    }
}
