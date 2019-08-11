using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyFirstMonoGame.Presentation;

namespace MyFirstMonoGame.Maps
{
    public class DungeonPassage : Map
    {
        public DungeonPassage(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
            Texture2D dungeon, Texture2D boss) :
            base(texture, rows, columns, enemyTexture, buttonTexture, font, dungeon, boss)
        {
            this.MapCubes = getMapCubes();
            getBoundingBoxes();
            DungeonBoxes.Add(new BoundingBox(new Vector3(703, 181, 0), new Vector3(737, 224, 0)));

            Level = 3;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.casual;
            Id = 3;
            North = 4;
            East = 0;
            South = 2;
            West = 0;
            StartingPoints = new List<Vector2>() { new Vector2(400, 450), new Vector2(725, 243), new Vector2(480, 20) };
            RespawnPoint = new Vector2(365, 200);

            Enemies = new List<Enemy>() { new Enemy(enemyTexture, 20f, new Vector2(390, 385), new Vector2(395, 385), new Vector2(375, 365)),
            new Enemy(enemyTexture, 23f, new Vector2(400, 322), new Vector2(525, 322), new Vector2(370, 322)),
            new Enemy(enemyTexture, 25f, new Vector2(650, 260), new Vector2(565, 257), new Vector2(700, 262)),
            new Enemy(enemyTexture, 35f, new Vector2(350, 150), new Vector2(350, 90), new Vector2(350, 210)),
            new Enemy(enemyTexture, 25f, new Vector2(210, 85), new Vector2(230, 100), new Vector2(185, 25)),
            };
            createCombatBoxes();
        }

        public override int DungeonId(Vector2 position)
        {
            return 101;
        }

        public override Vector2 GetStartingPoint(int key)
        {
            switch(key)
            {
                case 7: return StartingPoints[1];
                case 1: return StartingPoints[2];
                default: return StartingPoints[0];
            }
        }

        private List<int> getMapCubes()
        {
            var list = new List<int>()
            {
            1, 1, 7, 7, 0, 0, 0, 0, 1, 1, 1, 1, 1, 7, 7, 7, 1, 1, 1, 1, 0, 1, 0, 0, 0,
            1, 1, 1, 7, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 1, 1, 0, 0, 0, 1, 1,
            1, 1, 1, 7, 1, 1, 7, 7, 1, 1, 1, 7, 1, 7, 7, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
            1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 1, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 7, 7, 7, 7, 7, 7, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 7, 1, 1, 7, 1, 1, 7, 7, 7, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 7, 1, 7, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };

            return list;
        }
    }
}
