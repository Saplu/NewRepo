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
    public class Castle : Dungeon
    {
        public Castle(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font,
        Texture2D dungeon, Texture2D boss) : base(texture, rows, columns, enemyTexture, buttonTexture, font,
        dungeon, boss)
        {
            MapCubes = getMapCubes();
            getBoundingBoxes();
            DungeonBoxes.Add(new BoundingBox(new Vector3(32, 438, 0), new Vector3(64, 480, 0)));
            DungeonBoxes.Add(new BoundingBox(new Vector3(700, 400, 0), new Vector3(735, 455, 0)));

            Level = 7;
            MapDifficulty = CharacterClassLibrary.Enums.MissionDifficulty.hc;
            Id = 4;
            North = 0;
            East = 0;
            South = 0;
            West = 0;
            StartingPoints = new List<Vector2>() { new Vector2(50, 420) };
            RespawnPoint = new Vector2(50, 420);

            Enemies = new List<Enemy>() { new Enemy(enemyTexture, 20f, new Vector2(60, 250), new Vector2(30, 295), new Vector2(130, 225)),
            new Enemy(enemyTexture, 21f, new Vector2(60, 145), new Vector2(170, 155), new Vector2(30, 135)),
            new Enemy(enemyTexture, 23f, new Vector2(150, 40), new Vector2(190, 70), new Vector2(150, 40)),
            new Enemy(enemyTexture, 28f, new Vector2(345, 80), new Vector2(345, 70), new Vector2(395, 150)),
            new Enemy(enemyTexture, 23f, new Vector2(220, 340), new Vector2(220, 320), new Vector2(220, 380)),
            new Enemy(enemyTexture, 25f, new Vector2(350, 350), new Vector2(420, 290), new Vector2(350, 360)),
            new Enemy(enemyTexture, 28f, new Vector2(575, 345), new Vector2(575, 420), new Vector2(575, 280)),
            new Enemy(enemyTexture, 19f, new Vector2(505, 60), new Vector2(500, 30), new Vector2(515, 100)),
            new Enemy(enemyTexture, 26f, new Vector2(700, 50), new Vector2(745, 40), new Vector2(630, 60)),
            new Enemy(enemyTexture, 20f, new Vector2(638, 140), new Vector2(638, 205), new Vector2(638, 120)),
            };
            createCombatBoxes();
            Bosses = new List<Boss>() { new Boss(boss, new Vector2(314, 194), 4),
            new Boss(boss, new Vector2(520, 155), 5),
            new Boss(boss, new Vector2(690, 320), 6)};
            createBossBoxes();
            CombatBoxes.AddRange(BossBoxes);
        }

        public override int IsBossFight(BoundingBox heroBox)
        {
            foreach (var boss in Bosses)
            {
                if (heroBox.Intersects(boss.AggroBox))
                    return boss.Id;
            }
            return 0;
        }

        public override int DungeonId(Vector2 position)
        {
            return 4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateDifficulty();

        }

        private List<int> getMapCubes()
        {
            var list = new List<int>()
            {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 1,
            1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 1,
            1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 7, 7, 1, 1, 1, 7, 1, 1, 1, 1, 1, 7, 1, 1,
            1, 7, 7, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1,
            1, 7, 7, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1,
            1, 7, 1, 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1, 7, 1, 1, 1, 1,
            1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1,
            1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 7, 1,
            1, 7, 7, 7, 7, 1, 1, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 1, 7, 1, 7, 7, 7, 7, 1,
            1, 1, 1, 1, 7, 1, 7, 7, 1, 7, 7, 7, 7, 7, 1, 7, 7, 1, 7, 1, 7, 7, 7, 7, 1,
            1, 7, 7, 7, 7, 1, 7, 7, 1, 7, 7, 7, 7, 7, 1, 7, 7, 1, 7, 1, 7, 7, 7, 7, 1,
            1, 7, 7, 7, 7, 1, 7, 7, 1, 1, 1, 1, 7, 1, 1, 7, 7, 7, 7, 1, 7, 7, 1, 1, 1,
            1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1, 7, 7, 7, 7, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            };

            return list;
        }

        private void updateDifficulty()
        {
            switch(Enemies.Count())
            {
                case 6: Level = 8; break;
                case 3: Level = 9; break;
            }
        }
    }
}
