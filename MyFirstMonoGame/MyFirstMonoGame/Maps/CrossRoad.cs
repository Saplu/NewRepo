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
    public class CrossRoad : Map
    {
        public CrossRoad(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
            Texture2D dungeon, Texture2D boss) :
            base(texture, rows, columns, enemyTexture, buttonTexture, font, dungeon, boss)
        {
            this.MapCubes = getMapCubes();
            getBoundingBoxes();
            //var combatX = new List<int>() { 300, 350, 420, 470, 200, 160 };
            //var combatY = new List<int>() { 200, 250, 50, 80, 220, 190 };
            //getCombatBoxes(combatX, combatY);
            
            Level = 2;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.casual;
            Id = 2;
            North = 3;
            East = 0;
            South = 0;
            West = 1;

            StartingPoints[3] = new Vector2(400, 20);
            RespawnPoint = new Vector2(300, 300);

            Enemies = new List<Enemy>() { new Enemy(enemyTexture, 25f, new Vector2(160, 235), new Vector2(180, 290), new Vector2(140, 200)),
            new Enemy(enemyTexture, 20f, new Vector2(250, 225), new Vector2(305, 215), new Vector2(205, 235)),
            new Enemy(enemyTexture, 35f, new Vector2(700, 200), new Vector2(760, 210), new Vector2(60, 240)),
            new Enemy(enemyTexture, 24f, new Vector2(360, 250), new Vector2(404, 285), new Vector2(320, 215)),
            new Enemy(enemyTexture, 20f, new Vector2(440, 62), new Vector2(490, 65), new Vector2(410, 56)),
            new Enemy(enemyTexture, 26f, new Vector2(420, 115), new Vector2(380, 105), new Vector2(510, 145)),
            new Enemy(enemyTexture, 31f, new Vector2(600, 250), new Vector2(690, 260), new Vector2(510, 235)),
            new Enemy(enemyTexture, 17f, new Vector2(20, 100), new Vector2(40, 120), new Vector2(5, 75)),
            };
            createCombatBoxes();
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

            list[0] = 0; list[1] = 0; list[2] = 0; list[25] = 0; list[26] = 0;
            list[12] = 7; list[13] = 7; list[14] = 7; list[15] = 7;list[16] = 0; list[17] = 0; list[18] = 0;
            list[37] = 7; list[38] = 7; list[39] = 7; list[40] = 7; list[41] = 0;
            list[50] = 7; list[51] = 7; list[75] = 7; list[76] = 7; list[100] = 7; list[101] = 7;
            list[63] = 7; list[64] = 7; list[65] = 7; list[66] = 0; list[67] = 0; list[68] = 0;
            list[87] = 7; list[88] = 7; list[89] = 7; list[90] = 7;
            list[112] = 7; list[113] = 7; list[114] = 7; list[115] = 7; list[116] = 7;

            

            return list;
        }
    }
}
