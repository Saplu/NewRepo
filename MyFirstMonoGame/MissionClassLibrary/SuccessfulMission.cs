using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;
using CharacterClassLibrary.Enums;

namespace MissionClassLibrary
{
    public class SuccessfulMission
    {
        private Mission mission;
        private List<Player> survivors, players;
        private CharacterClassLibrary.Item loot;
        private int xp;


        public Mission Mission { get => mission; set => mission = value; }
        public List<Player> Survivors { get => survivors; set => survivors = value; }
        public Item Loot { get => loot; set => loot = value; }
        public int Xp { get => xp; set => xp = value; }
        public List<Player> Players { get => players; set => players = value; }

        public SuccessfulMission(Mission mission)
        {
            this.mission = mission;
            xp = CalculateXp();
            survivors = getSurvivors();
            Players = getPlayers();
            var quality = rewardItemQuality();
            var item = new RandomItemGenerator();
            loot = item.CreateItem(mission.Level, quality);
        }

        public void ModifyXp()
        {
            foreach(var player in survivors)
            {
                player.Xp += xp;
                player.CheckForLevelUp();
            }
        }

        private int CalculateXp()
        {
            var playerLevel = 0;
            var enemyLevel = 0;
            var value = mission.Enemies.Count * 5;
            foreach (var player in mission.Players)
                playerLevel += player.Level;
            foreach (var enemy in mission.Enemies)
            {
                var bonus = typeWeight(Convert.ToInt32(enemy.Type));
                enemyLevel += enemy.Level + bonus;
            }
            value += enemyLevel - playerLevel;
            if (value > 0)
                return value;
            else return 0;
        }

        private ItemQuality rewardItemQuality()
        {
            var rand = Utils.RandomProvider.GetRandom(1, 100);
            if (rand <= mission.RewardTable[0])
                return ItemQuality.Poor;
            else if (rand > mission.RewardTable[0] && rand <= mission.RewardTable[0] + mission.RewardTable[1])
                return ItemQuality.Good;
            else if (rand > mission.RewardTable[0] + mission.RewardTable[1] && rand <= mission.RewardTable[0] + mission.RewardTable[1] + mission.RewardTable[2])
                return ItemQuality.Great;
            else return ItemQuality.Masterpiece;
        }

        private int typeWeight(int type)
        {
            switch (type)
            {
                case 0: return -1;
                case 1: return 0;
                case 2: return 2;
                case 3: return 4;
                default: return 0;
            }
        }

        private List<Player> getSurvivors()
        {
            var survivors = new List<Player>();
            foreach (var player in mission.Players)
            {
                if (player.Health > 0)
                    survivors.Add(player);
            }
            return survivors;
        }

        private List<Player> getPlayers()
        {
            var players = new List<Player>();
            foreach (var player in mission.Players)
            {
                players.Add(player);
            }
            return players;
        }
    }
}
