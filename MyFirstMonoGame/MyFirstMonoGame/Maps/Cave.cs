using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MissionClassLibrary.Missions;

namespace MyFirstMonoGame.Maps
{
    public class Cave : Dungeon
    {
        public Cave(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
            Texture2D dungeon, Texture2D boss) : base(texture, rows, columns, enemyTexture, buttonTexture, font,
                dungeon, boss)
        {
            MapCubes = getMapCubes();
            getBoundingBoxes();
            var combatX = new List<int>() { 480, 150, 40, 40, 160, 450, 250, 735, 638 };
            var combatY = new List<int>() { 250, 420, 340, 100, 75, 100, 340, 160, 140 };
            getCombatBoxes(combatX, combatY);
            var bossX = new List<int>() { 350, 380, 720 };
            var bossY = new List<int>() { 170, 340, 350 };
            getBossBoxes(bossX, bossY);
            CombatBoxes.AddRange(BossBoxes);
            DungeonBoxes.Add(new BoundingBox(new Vector3(0, 450, 0), new Vector3(25, 480, 0)));
            DungeonBoxes.Add(new BoundingBox(new Vector3(700, 400, 0), new Vector3(735, 455, 0)));

            Level = 4;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.dungeon;
            Id = 3;
            North = 0;
            East = 0;
            South = 0;
            West = 0;
            StartingPoints = new List<Vector2>() { new Vector2(50, 440) };
            RespawnPoint = new Vector2(50, 440);
            bossFightId = 1;
        }

        public override int IsBossFight(BoundingBox heroBox)
        {
            var id = bossFightId;
            foreach(var box in BossBoxes)
            {
                if (heroBox.Intersects(box))
                    return id;
                id++;
            }
            return 0;
        }

        public override int DungeonId(Vector2 position)
        {
            return 3;
        }

        private List<int> getMapCubes()
        {
            var list = new List<int>()
            {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 1, 1, 1, 1, 7, 7, 7, 1,
            1, 7, 7, 7, 7, 7, 7, 1, 1, 7, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 7, 7, 7, 1,
            1, 7, 1, 1, 1, 7, 7, 1, 1, 7, 1, 7, 7, 1, 1, 7, 1, 1, 1, 1, 7, 7, 1, 7, 1,
            1, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 1, 7, 1, 7, 1, 1, 1, 7, 7, 1, 1, 7, 1,
            1, 7, 7, 7, 1, 1, 1, 1, 1, 1, 7, 7, 1, 7, 7, 7, 1, 1, 1, 1, 7, 1, 7, 7, 1,
            1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 7, 7, 7, 7, 1, 1, 7, 1,
            1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 1, 1, 1, 7, 7, 7, 1,
            1, 1, 7, 7, 1, 1, 1, 1, 7, 7, 7, 1, 1, 1, 1, 1, 7, 7, 1, 1, 1, 7, 1, 1, 1,
            1, 7, 7, 7, 1, 1, 1, 1, 7, 1, 7, 1, 7, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1,
            1, 7, 1, 1, 1, 7, 7, 7, 7, 1, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 1,
            7, 7, 1, 1, 1, 7, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 1,
            7, 7, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1,
            7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };

            return list;
        }
    }
}
