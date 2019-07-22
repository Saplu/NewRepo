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
        List<List<int>> healths;
        List<int> attackers;
        List<List<int>> playerTargets;

        public List<List<int>> Targets { get => targets; set => targets = value; }
        public List<List<int>> Healths { get => healths; set => healths = value; }
        public List<int> Attackers { get => attackers; set => attackers = value; }
        public List<List<int>> PlayerTargets { get => playerTargets; set => playerTargets = value; }

        public Logger()
        {
            targets = new List<List<int>>();
            healths = new List<List<int>>();
            attackers = new List<int>();
            playerTargets = new List<List<int>>();
        }

        public void UpdateHp(List<Player> players, List<NPC> enemies)
        {
            var list = new List<int>();
            foreach(var player in players)
                list.Add(player.Health);
            foreach (var enemy in enemies)
                list.Add(enemy.Health);
            healths.Add(list);
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
