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
    public class Islands : Map
    {
        public Islands(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
        Texture2D dungeon, Texture2D boss) :
        base(texture, rows, columns, enemyTexture, buttonTexture, font, dungeon, boss)
        {
            this.MapCubes = getMapCubes();
            getBoundingBoxes();

            Level = 5;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.casual;
            Id = 4;
            North = 0;
            East = 0;
            South = 3;
            West = 0;
            StartingPoints = new List<Vector2>() { new Vector2(450, 440) };
            RespawnPoint = new Vector2(450, 440);
            DungeonBoxes.Add(new BoundingBox(new Vector3(95, 20, 0), new Vector3(130, 63, 0)));

            Enemies = new List<Enemy>() { new Enemy(enemyTexture, 20f, new Vector2(450, 350), new Vector2(410, 350), new Vector2(455, 350)),
                new Enemy(enemyTexture, 20f, new Vector2(450, 230), new Vector2(490, 184), new Vector2(450, 230)),
                new Enemy(enemyTexture, 30f, new Vector2(300, 100), new Vector2(250, 100), new Vector2(350, 100)),
                new Enemy(enemyTexture, 28f, new Vector2(200, 130), new Vector2(230, 130), new Vector2(80, 130)),
                new Enemy(enemyTexture, 20f, new Vector2(80, 140), new Vector2(80, 225), new Vector2(80, 140)),
                new Enemy(enemyTexture, 23f, new Vector2(50, 300), new Vector2(80, 270), new Vector2(35, 350)),
            };
            createCombatBoxes();
        }

        public override Vector2 GetStartingPoint(int key)
        {
            switch(key)
            {
                default: return StartingPoints[0];
            }
        }

        public override int DungeonId(Vector2 position)
        {
            return 102;
        }

        private List<int> getMapCubes()
        {
            var list = new List<int>()
            {
                0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 7, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 7, 7, 7, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 7, 7, 0, 7, 1, 1, 1, 7, 7, 7, 7, 1, 1, 7, 7, 1, 7, 7, 0, 0, 0, 0, 0, 0,
                1, 1, 7, 7, 7, 7, 7, 7, 7, 0, 0, 7, 7, 7, 7, 1, 1, 7, 7, 0, 0, 0, 0, 0, 0,
                1, 1, 7, 7, 7, 0, 0, 7, 7, 0, 0, 7, 7, 0, 7, 1, 7, 7, 7, 0, 0, 0, 0, 0, 0,
                0, 0, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 7, 7, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 7, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 7, 7, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 7, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 7, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 7, 7, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 7, 7, 0, 0, 0, 0, 0, 1, 1, 1, 1, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };


            return list;
        }
    }
}
