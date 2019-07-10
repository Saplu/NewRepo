using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class FirstChallenge : Mission
    {
        public FirstChallenge(List<Player> players)
        {
            var enemy1 = new CharacterClassLibrary.NPCClasses.Rabbit(2, 1);
            var enemy2 = new CharacterClassLibrary.NPCClasses.Rabbit(2, 1);
            var enemy3 = new CharacterClassLibrary.NPCClasses.Rabbit(2, 1);
            var enemy4 = new CharacterClassLibrary.NPCClasses.Rabbit(2, 1);
            Enemies = new List<NPC>() { enemy1, enemy2, enemy3, enemy4 };
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
            Level = 1;
            ActionsTaken = new List<int>();
            RewardTable = new int[4] { 75, 25, 0, 0 };
            TransferTo = "Menu";
        }
    }
}
