using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CharacterClassLibrary.Enums;

namespace MyFirstMonoGame
{
    public class Map
    {
        private Texture2D texture, enemyTexture;
        private int rows;
        private int columns;
        private List<Rectangle> mapParts;
        private List<int> mapCubes;
        private int mapRows = 15;
        private int mapColumns = 25;
        private int currentFrame;
        private List<BoundingBox> boxes;
        private List<BoundingBox> combatBoxes;
        private int level;
        private CharacterClassLibrary.Enums.MissionDifficulty mapDifficulty;

        public List<int> MapCubes { get => mapCubes; set => mapCubes = value; }
        public Texture2D Texture { get => texture; }
        public int Rows { get => rows; }
        public int Columns { get => columns; }
        public List<BoundingBox> Boxes { get => boxes; }
        public List<BoundingBox> CombatBoxes { get => combatBoxes; set => value = combatBoxes; }
        public Texture2D EnemyTexture { get => enemyTexture; set => enemyTexture = value; }
        public int Level { get => level; set => level = value; }
        public MissionDifficulty MapDifficulty { get => mapDifficulty; set => mapDifficulty = value; }

        public Map(Texture2D texture, int rows, int columns, Texture2D enemyTexture)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.currentFrame = 0;
            this.boxes = new List<BoundingBox>();
            //Koko on 800x480. 25x15 32x32 tiiliä.
            mapParts = getMapParts();
            this.enemyTexture = enemyTexture;
            combatBoxes = new List<BoundingBox>();

        }

        public void Draw(SpriteBatch sprite) //Kantaluokkaan
        {
            for (int i = 0; i < mapCubes.Count; i++)
            {
                var row = (int)((float)currentFrame / (float)mapColumns);
                var column = currentFrame % mapColumns;
                var destinationRectangle = new Rectangle(32 * column, 32 * row, 32, 32);
                var destination = new Vector2(destinationRectangle.X, destinationRectangle.Y);
                currentFrame++;
                sprite.Draw(Texture, destinationRectangle, mapParts[mapCubes[i]], Color.White);
            }
            currentFrame = 0;

            foreach(var box in combatBoxes)
            {
                sprite.Draw(enemyTexture, new Rectangle((int)box.Min.X, (int)box.Min.Y, (int)box.Max.X - (int)box.Min.X, 
                    (int)box.Max.Y - (int)box.Min.Y), Color.White);
            }
        }

        public void RemoveDestroyedEnemy(Hero hero)
        {
            var keepList = new List<BoundingBox>();
            foreach (var enemy in CombatBoxes)
            {
                if (!hero.Box.Intersects(enemy))
                    keepList.Add(enemy);
            }
            combatBoxes = keepList;
        }

        private List<Rectangle> getMapParts() //Kantaluokkaan
        {
            var parts = new List<Rectangle>();
            var frameWidth = texture.Width / columns;
            var frameHeight = texture.Height / rows;
            var currentRow = 0;
            var currentColumn = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int u = 0; u < columns; u++)
                {
                    var part = new Rectangle(texture.Width / columns * currentColumn, texture.Height / rows * currentRow, frameWidth, frameHeight);
                    parts.Add(part);
                    if (currentColumn == columns - 1)
                        currentColumn = 0;
                    else currentColumn++;
                }
                if (currentRow == rows - 1)
                    currentRow = 0;
                else currentRow++;
            }
            return parts;
        }


        protected void getBoundingBoxes() //Kaikki tästä alaspäin on kantaluokkakamaa.
        {
            var list = new List<BoundingBox>();
            for (int i = 0; i < mapCubes.Count; i++)
            {
                if (mapCubes[i] == 0 || mapCubes[i] == 1 || mapCubes[i] == 5)
                {
                    var x = i % 25 * 32;
                    var y = i / 25 * 32;
                    var box = new BoundingBox(new Vector3(x, y, 0), new Vector3(x + 32, y + 32, 0));
                    list.Add(box);
                }
            }
            addAdjacentBoxes(list);
        }

        private void addAdjacentBoxes(List<BoundingBox> list)
        {
            var combinedList = new List<BoundingBox>();
            for (int i = 0; i < list.Count; i++)
            {
                var xmin = list[i].Min.X;
                var ymin = list[i].Min.Y;
                var ymax = list[i].Max.Y;
                if (list[i] == list.Last())
                {
                    var xmax = list[i].Max.X;
                    combinedList.Add(new BoundingBox(new Vector3(xmin, ymin, 0), new Vector3(xmax, ymax, 0)));
                }
                else if (list[i].Max.X == list[i + 1].Min.X)
                {
                    var xmax = list[i + 1].Max.X;
                    combinedList.Add(new BoundingBox(new Vector3(xmin, ymin, 0), new Vector3(xmax, ymax, 0)));
                    i++;
                }
                else
                {
                    var xmax = list[i].Max.X;
                    combinedList.Add(new BoundingBox(new Vector3(xmin, ymin, 0), new Vector3(xmax, ymax, 0)));
                }
            }
            if (combinedList.Count != list.Count)
                addAdjacentBoxes(combinedList);
            else boxes = combinedList;
        }

        protected List<int> rowOfWater() //Nämä kantaluokkaan muiden vastaavien kanssa.
        {
            return new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        protected List<int> rowOfGrass()
        {
            return new List<int>() { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
        }

        protected List<int> rowOfStoneWall()
        {
            return new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }

        protected List<int> rowOfBrickWall()
        {
            return new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        }
    }
}
