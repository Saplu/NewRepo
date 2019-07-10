using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary
{
    public class RandomMission : Mission
    {
        public RandomMission(List<NPC> enemies, List<Player> players, int level, int[] rewardTable)
        {
            Enemies = enemies;
            Players = players;
            Level = level;
            RewardTable = rewardTable;
            Turn = 1;
            ActionsTaken = new List<int>();

            foreach (var enemy in Enemies)
            {
                var numb = Enemies.IndexOf(enemy);
                enemy.Position = numb + 5;
            }
            foreach (var player in Players)
            {
                var numb = Players.IndexOf(player);
                player.Position = numb + 1;
            }
        }
    }
}
