using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
            var combatX = new List<int>() { 405, 650, 505 };
            var combatY = new List<int>() { 385, 260, 355 };
            getCombatBoxes(combatX, combatY);
            DungeonBoxes.Add(new BoundingBox(new Vector3(703, 181, 0), new Vector3(737, 224, 0)));

            Level = 3;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.casual;
            Id = 3;
            North = 0;
            East = 0;
            South = 2;
            West = 0;
            StartingPoints = new List<Vector2>() { new Vector2(400, 450), new Vector2(725, 243) };
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
                default: return StartingPoints[0];
            }
        }

        private List<int> getMapCubes()
        {
            var list = new List<int>()
            {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1,
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
