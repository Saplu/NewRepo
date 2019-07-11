using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstMonoGame.Maps
{
    public class Training : Map
    {
        public Training(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font) : 
            base(texture, rows, columns, enemyTexture, buttonTexture, font)
        {
            this.MapCubes = getMapCubes();
            getBoundingBoxes();
            CombatBoxes.Add (new BoundingBox(new Vector3(300, 200, 0), new Vector3(332, 232, 0)));
            CombatBoxes.Add(new BoundingBox(new Vector3(350, 250, 0), new Vector3(382, 282, 0)));
            CombatBoxes.Add(new BoundingBox(new Vector3(250, 270, 0), new Vector3(282, 302, 0)));
            Level = 1;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.easy;
            Id = 1;
            North = 0;
            East = 2;
            South = 0;
            West = 0;
        }

        private List<int> getMapCubes() //Tämä on karttakohtainen!
        {
            var list = new List<int>();

            var water = rowOfWater();
            var grass = rowOfGrass();
            var stone = rowOfStoneWall();
            var brick = rowOfBrickWall();

            list.AddRange(water);
            list.AddRange(water);
            list.AddRange(grass);
            list.AddRange(stone);
            list.AddRange(stone);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(brick);
            list.AddRange(brick);
            list.AddRange(grass);
            list.AddRange(grass);
            list.AddRange(grass);

            list[77] = 7;
            list[102] = 7;
            list[150] = 5;
            list[151] = 5;
            list[156] = 5;
            
            list[170] = 5;
            list[99] = 7;
            list[124] = 7;

            return list;
        }
    }
}
