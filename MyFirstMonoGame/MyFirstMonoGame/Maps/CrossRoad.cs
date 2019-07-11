using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstMonoGame.Maps
{
    public class CrossRoad : Map
    {
        public CrossRoad(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font) :
            base(texture, rows, columns, enemyTexture, buttonTexture, font)
        {
            this.MapCubes = getMapCubes();
            getBoundingBoxes();
            CombatBoxes.Add(new BoundingBox(new Vector3(300, 200, 0), new Vector3(332, 232, 0)));
            CombatBoxes.Add(new BoundingBox(new Vector3(350, 250, 0), new Vector3(382, 282, 0)));
            Level = 2;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.casual;
            Id = 2;
            North = 0;
            East = 0;
            South = 0;
            West = 1;
        }

        private List<int> getMapCubes() //374 on viimonen, 25x15
        {
            var list = new List<int>();

            var water = rowOfWater();
            var grass = rowOfGrass();
            var stone = rowOfStoneWall();
            var brick = rowOfBrickWall();

            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(stone);

            //list[12] = 

            return list;
        }
    }
}
