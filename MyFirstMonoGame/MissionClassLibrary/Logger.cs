using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary
{
    public class Logger
    {
        List<List<int>> targets;
        List<int[]> healths;
        List<int> attackers;
        List<List<int>> playerTargets;

        public List<List<int>> Targets { get => targets; set => targets = value; }
        public List<int[]> Healths { get => healths; set => healths = value; }
        public List<int> Attackers { get => attackers; set => attackers = value; }
        public List<List<int>> PlayerTargets { get => playerTargets; set => playerTargets = value; }

        public Logger()
        {
            targets = new List<List<int>>();
            healths = new List<int[]>();
            attackers = new List<int>();
            playerTargets = new List<List<int>>();
        }

        public void UpdateHp(List<Player> players, List<NPC> enemies)
        {
            var arr = new int[8];
            foreach(var player in players)
                arr[player.Position - 1] = player.Health;
            foreach (var enemy in enemies)
                arr[enemy.Position - 1] = enemy.Health;
            healths.Add(arr);
        }

        public void Update(int enemyIndex, List<int> _targets)
        {
            attackers.Add(enemyIndex + 5);
            targets.Add(_targets);
        }

        public void Clear()
        {
            targets.Clear();
            healths.Clear();
            attackers.Clear();
        }
    }
}
