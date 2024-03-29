﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MissionClassLibrary.Missions;
using MyFirstMonoGame.Presentation;

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
            
            Enemies = new List<Enemy>() { new Enemy(enemyTexture, 20f, new Vector2(480, 250), new Vector2(480, 180), new Vector2(480, 270)),
            new Enemy(enemyTexture, 21f, new Vector2(150, 420), new Vector2(90, 420), new Vector2(170, 420)),
            new Enemy(enemyTexture, 23f, new Vector2(40, 340), new Vector2(40, 365), new Vector2(30, 315)),
            new Enemy(enemyTexture, 18f, new Vector2(40, 150), new Vector2(30, 100), new Vector2(40, 200)),
            new Enemy(enemyTexture, 20f, new Vector2(160, 75), new Vector2(120, 65), new Vector2(200, 150)),
            new Enemy(enemyTexture, 25f, new Vector2(450, 100), new Vector2(415, 70), new Vector2(465, 101)),
            new Enemy(enemyTexture, 17f, new Vector2(250, 345), new Vector2(220, 380), new Vector2(250, 345)),
            new Enemy(enemyTexture, 30f, new Vector2(735, 160), new Vector2(735, 60), new Vector2(735, 250)),
            new Enemy(enemyTexture, 20f, new Vector2(638, 140), new Vector2(638, 205), new Vector2(638, 135)),
            };
            createCombatBoxes();
            Bosses = new List<Boss>() { new Boss(boss, new Vector2(350, 170), 1),
            new Boss(boss, new Vector2(380, 340), 2),
            new Boss(boss, new Vector2(720, 350), 3)};
            createBossBoxes();
            CombatBoxes.AddRange(BossBoxes);
        }

        public override int IsBossFight(BoundingBox heroBox)
        {
            foreach(var boss in Bosses)
            {
                if (heroBox.Intersects(boss.AggroBox))
                    return boss.Id;
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
