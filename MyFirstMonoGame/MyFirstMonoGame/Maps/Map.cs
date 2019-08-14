using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CharacterClassLibrary.Enums;
using MyFirstMonoGame.Presentation;

namespace MyFirstMonoGame
{
    public class Map
    {
        private Texture2D texture, enemyTexture, dungeonTexture, buttonTexture, bossTexture;
        private int rows;
        private int columns;
        private List<Rectangle> mapParts;
        private List<int> mapCubes;
        private int mapRows = 15;
        private int mapColumns = 25;
        private int currentFrame;
        private List<BoundingBox> boxes;
        private List<BoundingBox> combatBoxes;
        private List<BoundingBox> dungeonBoxes;
        private int level;
        private CharacterClassLibrary.Enums.MissionDifficulty mapDifficulty;
        private Presentation.Button menuButton;
        private SpriteFont font;
        private int id, north, east, south, west;
        private List<Vector2> startingPoints;
        private Vector2 respawnPoint;
        private List<Presentation.Enemy> enemies;

        public List<int> MapCubes { get => mapCubes; set => mapCubes = value; }
        public Texture2D Texture { get => texture; }
        public int Rows { get => rows; }
        public int Columns { get => columns; }
        public List<BoundingBox> Boxes { get => boxes; }
        public List<BoundingBox> CombatBoxes { get => combatBoxes; set => value = combatBoxes; }
        public Texture2D EnemyTexture { get => enemyTexture; set => enemyTexture = value; }
        public int Level { get => level; set => level = value; }
        public MissionDifficulty MapDifficulty { get => mapDifficulty; set => mapDifficulty = value; }
        public int North { get => north; set => north = value; }
        public int East { get => east; set => east = value; }
        public int South { get => south; set => south = value; }
        public int West { get => west; set => west = value; }
        public int Id { get => id; set => id = value; }
        public List<Vector2> StartingPoints { get => startingPoints; set => startingPoints = value; }
        public List<BoundingBox> DungeonBoxes { get => dungeonBoxes; set => dungeonBoxes = value; }
        public Texture2D DungeonTexture { get => dungeonTexture; set => dungeonTexture = value; }
        public Vector2 RespawnPoint { get => respawnPoint; set => respawnPoint = value; }
        public List<Enemy> Enemies { get => enemies; set => enemies = value; }

        public Map(Texture2D texture, int rows, int columns, Texture2D enemyTexture, Texture2D buttonTexture, SpriteFont font, Texture2D dungeon, 
            Texture2D bossTexture)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.currentFrame = 0;
            this.boxes = new List<BoundingBox>();
            this.buttonTexture = buttonTexture;
            startingPoints = new List<Vector2>() { new Vector2(20, 300), new Vector2(20, 300), new Vector2(20, 300), new Vector2(20, 300) };
            //Koko on 800x480. 25x15 32x32 tiiliä.
            mapParts = getMapParts();
            this.enemyTexture = enemyTexture;
            menuButton = new Button(buttonTexture, 735, 450, "Menu", 60, 25);
            this.font = font;
            dungeonBoxes = new List<BoundingBox>();
            this.dungeonTexture = dungeon;
            this.bossTexture = bossTexture;
            enemies = new List<Enemy>();
            combatBoxes = new List<BoundingBox>();
            createCombatBoxes();
        }

        public Map Create(int key)
        {
            switch(key)
            {
                case 1: return new Maps.Training(texture, rows, columns, enemyTexture, buttonTexture, font, dungeonTexture, bossTexture);
                case 2: return new Maps.CrossRoad(texture, rows, columns, enemyTexture, buttonTexture, font, dungeonTexture, bossTexture);
                case 3: return new Maps.DungeonPassage(texture, rows, columns, enemyTexture, buttonTexture, font, dungeonTexture, bossTexture);
                case 4: return new Maps.Islands(texture, rows, columns, enemyTexture, buttonTexture, font, dungeonTexture, bossTexture);
                default: throw new Exception("No map found.");
            }
        }

        public Map CreateDungeon(int key)
        {
            switch(key)
            {
                case 101: return new Maps.Cave(texture, rows, columns, enemyTexture, buttonTexture, font, dungeonTexture, bossTexture);
                case 102: return new Maps.Castle(texture, rows, columns, enemyTexture, buttonTexture, font, dungeonTexture, bossTexture);
                default: return Create(key);
            }
        }

        public MissionClassLibrary.Mission CreateBossFight(int key, List<CharacterClassLibrary.Player> players)
        {
            switch(key)
            {
                case 1: return new MissionClassLibrary.Missions.Keep(players);
                case 2: return new MissionClassLibrary.Missions.Castle(players);
                case 3: return new MissionClassLibrary.Missions.ThroneRoom(players);
                case 4: return new MissionClassLibrary.Missions.CastleFirst(players);
                case 5: return new MissionClassLibrary.Missions.CastleSecond(players);
                default: throw new Exception("No boss found");
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var enemy in enemies)
                enemy.Update(gameTime);
            getCombatBoxes();
        }

        public virtual void Draw(SpriteBatch sprite) //Kantaluokkaan
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

            foreach(var box in dungeonBoxes)
            {
                sprite.Draw(dungeonTexture, new Rectangle((int)box.Min.X, (int)box.Min.Y, (int)box.Max.X - (int)box.Min.X,
                    (int)box.Max.Y - (int)box.Min.Y), Color.White);
            }
            foreach(var enemy in enemies)
            {
                enemy.Draw(sprite);
            }
            menuButton.Draw(sprite, font);
        }

        public virtual void RemoveDestroyedEnemy(Hero hero)
        {
            var keepList = new List<Enemy>();
            foreach (var enemy in enemies)
            {
                if (!hero.Box.Intersects(enemy.AggroBox))
                    keepList.Add(enemy);
            }
            enemies = keepList;
            createCombatBoxes();
            getBoundingBoxes();
        }

        public void UpdateButtons(MouseState currentState)
        {
            menuButton.UpdateMouseState(currentState);
        }

        public string CheckButtons()
        {
            if (menuButton.ButtonClicked())
                return "MainMenu";
            else return "Adventure";
        }

        public int NextMap(int key)
        {
            switch(key)
            {
                case 1: return north;
                case 2: return east;
                case 3: return south;
                case 4: return west;
                default: return 0;
            }
        }

        public virtual int DungeonId(Vector2 position)
        {
            return 0;
        }

        public virtual int IsBossFight(BoundingBox box)
        {
            return 0;
        }

        public virtual Vector2 GetStartingPoint(int key)
        {
            switch(key)
            {
                case 4: return startingPoints[0];
                case 3: return startingPoints[1];
                case 2: return startingPoints[2];
                case 1: return startingPoints[3];
                default: return startingPoints[0];
            }
        }

        private List<Rectangle> getMapParts()
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

        protected void getCombatBoxes(List<int> x, List<int> y)
        {
            var cboxes = new List<BoundingBox>();
            var index = 0;

            foreach(var item in x)
            {
                var box = new BoundingBox();
                box.Min.X = x[index];
                box.Min.Y = y[index];
                box.Max.X = x[index] + 32;
                box.Max.Y = y[index] + 32;
                cboxes.Add(box);
                index++;
            }
            combatBoxes = cboxes;
        }

        protected void getCombatBoxes()
        {
            var index = 0;
            foreach(var enemy in enemies)
            {
                combatBoxes[index] = enemy.AggroBox;
                index++;
            }
            
        }

        protected void createCombatBoxes()
        {
            combatBoxes = new List<BoundingBox>();
            foreach(var enemy in enemies)
            {
                combatBoxes.Add(enemy.AggroBox);
            }
        }
    }
}
