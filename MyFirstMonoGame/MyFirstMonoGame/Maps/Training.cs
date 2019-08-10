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
    public class Training : Map
    {
        public Training(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
            Texture2D dungeon, Texture2D boss) : 
            base(texture, rows, columns, enemyTexture, buttonTexture, font, dungeon, boss)
        {
            this.MapCubes = getMapCubes();
            getBoundingBoxes();
            //var combatX = new List<int>() { 160, 300, 350, 250, 430, 500, 620 };
            //var combatY = new List<int>() { 235, 200, 250, 270, 285, 210, 240 };
            //getCombatBoxes(combatX, combatY);
            Level = 1;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.easy;
            Id = 1;
            North = 0;
            East = 2;
            South = 0;
            West = 0;
            StartingPoints[2] = new Vector2(750, 300);
            RespawnPoint = new Vector2(400, 180);
            Enemies = new List<Enemy>() { new Enemy(enemyTexture, 20f, new Vector2(750, 100), new Vector2(740, 250), new Vector2(750, 80)),
            new Enemy(enemyTexture, 25f, new Vector2(160, 235), new Vector2(180, 300), new Vector2(140, 200))
            };
            createCombatBoxes();
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
            list[99] = 7; list[98] = 7;
            list[124] = 7; list[123] = 7;

            return list;
        }
    }
}
