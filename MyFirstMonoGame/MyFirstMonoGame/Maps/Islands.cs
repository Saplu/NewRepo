using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
            var combatX = new List<int>() {};
            var combatY = new List<int>() {};
            getCombatBoxes(combatX, combatY);

            Level = 5;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.casual;
            Id = 4;
            North = 0;
            East = 0;
            South = 3;
            West = 0;
            StartingPoints = new List<Vector2>() { new Vector2(450, 440) };
            RespawnPoint = new Vector2(450, 50);
        }

        public override Vector2 GetStartingPoint(int key)
        {
            switch(key)
            {
                default: return StartingPoints[0];
            }
        }

        private List<int> getMapCubes()
        {
            var list = new List<int>()
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
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
                0, 7, 7, 7, 0, 0, 0, 0, 0, 1, 1, 1, 1, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };


            return list;
        }
    }
}
