using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace MissionClassLibrary
{
    public class RandomMissionGenerator
    {
        int enemyCount;
        private List<int> enemyTypes;
        int missionDifficulty;
        int[] rewardTable;

        public RandomMissionGenerator()
        {
            enemyCount = 0;
            enemyTypes = new List<int>();
            missionDifficulty = 0;
            rewardTable = new int[4];
        }

        public Mission CreateMission(int level, MissionDifficulty difficulty, List<CharacterClassLibrary.Player> players)
        {
            var rand = Utils.RandomProvider.GetRandom(1, 100);
            var anotherRand = Utils.RandomProvider.GetRandom(1, 100);

            switch(difficulty)
            {
                case MissionDifficulty.easy: missionDifficulty = 0; break;
                case MissionDifficulty.casual: missionDifficulty = 1; break;
                case MissionDifficulty.dungeon: missionDifficulty = 2; break;
                case MissionDifficulty.hc: missionDifficulty = 3; break;
            }

            enemies(rand);
            types(anotherRand, enemyCount);
            createRewardTable();

            var enemyList = new List<CharacterClassLibrary.NPC>();
            var enemyGenerator = new CharacterClassLibrary.RandomEnemyGenerator();
            foreach(var thing in enemyTypes)
            {
                var type = (NPCType)Enum.Parse(typeof(NPCType), thing.ToString());
                enemyList.Add(enemyGenerator.CreateEnemy(type, level));
            }
            var mission = new RandomMission(enemyList, players, level, rewardTable);
            return mission;
        }

        private void enemies(int random)
        {
            if (missionDifficulty == 0)
            {
                if (random < 30)
                    enemyCount = 1;
                else if (random < 60)
                    enemyCount = 2;
                else if (random < 80)
                    enemyCount = 3;
                else enemyCount = 4;
            }
            if (missionDifficulty == 1)
            {
                if (random < 30)
                    enemyCount = 2;
                else if (random < 70)
                    enemyCount = 3;
                else enemyCount = 4;
            }
            if (missionDifficulty == 2)
            {
                if (random < 20)
                    enemyCount = 2;
                else if (random < 40)
                    enemyCount = 3;
                else enemyCount = 4;
            }
            if (missionDifficulty == 3)
            {
                if (random < 30)
                    enemyCount = 3;
                else enemyCount = 4;
            }
        }

        private void types(int random, int enemyCount)
        {
            switch(missionDifficulty)
            {
                case 0: switch(enemyCount)
                    {
                        case 1: enemyTypes.Add(2); break;
                        case 2: enemyTypes.Add(1); enemyTypes.Add(1); break;
                        case 3: enemyTypes = new List<int>() { 1, 0, 0 }; break;
                        case 4: enemyTypes = new List<int>() { 0, 0, 0, 0 }; break;
                    }break;
                case 1: switch(enemyCount)
                    {
                        case 2: enemyTypes = new List<int>() { 2, 1 }; break;
                        case 3: enemyTypes = new List<int>() { 2, 0, 0 }; break;
                        case 4: enemyTypes = new List<int>() { 1, 1, 1, 0 }; break;
                    }break;
                case 2: switch(enemyCount)
                    {
                        case 2: enemyTypes = new List<int>() { 3, 2 }; break;
                        case 3: enemyTypes = new List<int>() { 3, 1, 1 }; break;
                        case 4: enemyTypes = new List<int>() { 2, 2, 2, 1 }; break;
                    }break;
                case 3: switch(enemyCount)
                    {
                        case 3: enemyTypes = new List<int>() { 3, 3, 1 }; break;
                        case 4: enemyTypes = new List<int>() { 3, 2, 2, 1 }; break;
                    }break;
            }
        }
        
        private void createRewardTable()
        {
            switch(missionDifficulty)
            {
                case 0: rewardTable = new int[4] { 100, 0, 0, 0 }; break;
                case 1: rewardTable = new int[4] { 50, 40, 10, 0 }; break;
                case 2: rewardTable = new int[4] { 20, 40, 35, 5 }; break;
                case 3: rewardTable = new int[4] { 0, 40, 40, 20 }; break;
            }
        }
    }
}
