﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MissionClassLibrary;
using MyFirstMonoGame.Presentation;

namespace MyFirstMonoGame.Maps
{
    public class Dungeon : Map
    {
        List<BoundingBox> bossBoxes;
        List<Mission> bossFights;
        List<Presentation.Boss> bosses;
        Texture2D bossTexture;

        protected int bossFightId;

        public List<BoundingBox> BossBoxes { get => bossBoxes; set => bossBoxes = value; }
        public List<Mission> BossFights { get => bossFights; set => bossFights = value; }
        public List<Boss> Bosses { get => bosses; set => bosses = value; }

        public Dungeon(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
            Texture2D dungeon, Texture2D boss) :
            base(texture, rows, columns, enemyTexture, buttonTexture, font, dungeon, boss)
        {
            bossTexture = boss;
            bossFights = new List<Mission>();
            bossBoxes = new List<BoundingBox>();
            bosses = new List<Boss>();
        }

        public override void Draw(SpriteBatch sprite)
        {
            base.Draw(sprite);
            foreach(var box in bossBoxes)
            {
                sprite.Draw(bossTexture, new Rectangle((int)box.Min.X, (int)box.Min.Y, (int)box.Max.X - (int)box.Min.X, 
                    (int)box.Max.Y - (int)box.Min.Y), Color.White);
            }
        }

        public override void RemoveDestroyedEnemy(Hero hero)
        {
            base.RemoveDestroyedEnemy(hero);
            var keepList = new List<Boss>();
            foreach(var boss in bosses)
            {
                if (!hero.Box.Intersects(boss.AggroBox))
                    keepList.Add(boss);
            }
            bosses = keepList;
            createBossBoxes();
            CombatBoxes.AddRange(bossBoxes);
        }

        protected void getBossBoxes(List<int> X, List<int> Y)
        {
            var bBoxes = new List<BoundingBox>();
            var index = 0;

            foreach (var item in X)
            {
                var box = new BoundingBox();
                box.Min.X = X[index];
                box.Min.Y = Y[index];
                box.Max.X = X[index] + 32;
                box.Max.Y = Y[index] + 32;
                bBoxes.Add(box);
                index++;
            }
            bossBoxes = bBoxes;
        }

        protected void createBossBoxes()
        {
            bossBoxes = new List<BoundingBox>();
            foreach(var boss in bosses)
            {
                bossBoxes.Add(boss.AggroBox);
            }
        }

        private List<int> getMapCubes()
        {
            return new List<int>();
        }
    }
}
