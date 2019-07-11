using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyFirstMonoGame
{
    public class Hero
    {
        private Vector2 position;
        private Texture2D texture;
        private int rows;
        private int columns;
        private int currentFrame;
        private float speed;
        private float angle;
        private int frameMultiplier;
        private BoundingBox box;
        private int actionMultiplier;
        private bool previousActionLeft;
        public BoundingBox Box { get => box; }
        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Texture { get => texture; }
        public int Rows { get => rows; }
        public int Columns { get => columns; set => columns = value; }

        public Hero(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            currentFrame = 0;
            frameMultiplier = 0;
            speed = 150f;
            position = new Vector2(20, 300);
            box = new BoundingBox(new Vector3(position.X, position.Y, 0), new Vector3(position.X + 32, position.Y + 32, 0));
            actionMultiplier = 0;
            previousActionLeft = false;
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (previousActionLeft == true)
                    left();
                else right();
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (previousActionLeft == true)
                    left();
                else right();
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                left();
                previousActionLeft = true;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                right();
                previousActionLeft = false;
            }
            if (!kstate.IsKeyDown(Keys.Right) && !kstate.IsKeyDown(Keys.Left) && !kstate.IsKeyDown(Keys.Up) &&
                !kstate.IsKeyDown(Keys.Down))
            {
                if (previousActionLeft == true)
                    actionMultiplier = 0;
                else actionMultiplier = 4;
            }

            chooseFrame();

            position.X = Math.Min(Math.Max(10, Position.X), graphics.PreferredBackBufferWidth - 10);
            position.Y = Math.Min(Math.Max(16, Position.Y), graphics.PreferredBackBufferHeight - 16);

            box = new BoundingBox(new Vector3(position.X - texture.Width / Columns / 2, position.Y - texture.Height / Rows / 2, 0),
                new Vector3(position.X + texture.Width / Columns / 2, position.Y + texture.Height / Rows / 2, 0));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)(((float)currentFrame + actionMultiplier) / (float)Columns);
            int column = (currentFrame + actionMultiplier) % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
            Vector2 destination = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            Vector2 origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

            spriteBatch.Draw(Texture, destination, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void CheckForCollision(List<BoundingBox> boxes, GameTime gameTime, GraphicsDeviceManager graphics)
        {
            foreach (var item in boxes)
            {
                if (box.Intersects(item))
                    checkCollisionSide(item, gameTime, graphics);
            }
        }

        public string CheckForCombat(List<BoundingBox> enemies)
        {
            foreach(var enemy in enemies)
            {
                if (box.Intersects(enemy))
                    return "Combat";
            }
            return "Adventure";
        }

        public int OnBorder()
        {
            if (box.Max.X >= 799)
                return 2;
            else if (box.Min.X <= 1)
                return 4;
            else if (box.Max.Y >= 479)
                return 3;
            else if (box.Min.Y <= 1)
                return 1;
            else return 0;
        }

        public Vector2 NewMapPosition(int key)
        {
            switch(key)
            {
                case 1: return new Vector2(position.X, 460);
                case 2: return new Vector2(20, position.Y);
                case 3: return new Vector2(position.X, 20);
                case 4: return new Vector2(780, position.Y);
                default: throw new Exception("Something awkward just happened.");
            }
        }

        private void checkCollisionSide(BoundingBox enviroment, GameTime gameTime, GraphicsDeviceManager graphics)
        {
            var kstate = Keyboard.GetState();

            if (box.Max.X >= enviroment.Min.X && box.Max.X < enviroment.Max.X && box.Max.Y > enviroment.Min.Y + 5 && box.Min.Y + 5 < enviroment.Max.Y)
                revertMovement(gameTime, graphics, 1);
            if (box.Min.X <= enviroment.Max.X && box.Min.X > enviroment.Min.X && box.Max.Y > enviroment.Min.Y + 5 && box.Min.Y + 5 < enviroment.Max.Y)
                revertMovement(gameTime, graphics, 2);
            if (box.Min.Y <= enviroment.Max.Y && box.Min.Y > enviroment.Min.Y && box.Max.X > enviroment.Min.X + 5 && box.Min.X + 5 < enviroment.Max.X)
                revertMovement(gameTime, graphics, 3);
            if (box.Max.Y >= enviroment.Min.Y && box.Max.Y < enviroment.Max.Y && box.Max.X > enviroment.Min.X + 5 && box.Min.X + 5 < enviroment.Max.X)
                revertMovement(gameTime, graphics, 4);
        }

        private void revertMovement(GameTime gameTime, GraphicsDeviceManager graphics, int side)
        {
            var kstate = Keyboard.GetState();

            switch (side)
            {
                case 1: if (kstate.IsKeyDown(Keys.Right)) position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                case 2: if (kstate.IsKeyDown(Keys.Left)) position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                case 3: if (kstate.IsKeyDown(Keys.Up)) position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                case 4: if (kstate.IsKeyDown(Keys.Down)) position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
            }

            position.X = Math.Min(Math.Max(10, Position.X), graphics.PreferredBackBufferWidth - 10);
            position.Y = Math.Min(Math.Max(16, Position.Y), graphics.PreferredBackBufferHeight - 16);

            box = new BoundingBox(new Vector3(position.X - texture.Width / Columns / 2, position.Y - texture.Height / Rows / 2, 0),
                new Vector3(position.X + texture.Width / Columns / 2, position.Y + texture.Height / Rows / 2, 0));
        }

        private void chooseFrame()
        {
            frameMultiplier++;
            if (frameMultiplier == 6)
            {
                frameMultiplier = 0;
                currentFrame++;
                if (currentFrame == 4)
                    currentFrame = 0;
            }
        }

        private void left()
        {
            actionMultiplier = 12;
        }

        private void right()
        {
            actionMultiplier = 8;
        }
    }
}
