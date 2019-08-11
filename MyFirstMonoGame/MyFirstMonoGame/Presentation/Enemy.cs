using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyFirstMonoGame.Presentation
{
    public class Enemy
    {
        protected Texture2D texture;
        protected float speed;
        protected Vector2 position, firstEnd, secondEnd, destination;
        protected BoundingBox aggroBox;
        private int currentFrame, frameTime;
        
        public BoundingBox AggroBox { get => aggroBox; set => aggroBox = value; }

        public Enemy(Texture2D texture, float speed, Vector2 start, Vector2 first, Vector2 second)
        {
            this.texture = texture;
            this.speed = speed;
            position = start;
            firstEnd = first;
            secondEnd = second;
            destination = firstEnd;
            aggroBox = new BoundingBox(new Vector3(position.X, position.Y, 0), new Vector3(position.X + 32, position.Y + 32, 0));
            currentFrame = 0;
            frameTime = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (position.X < destination.X)
                move(0, gameTime);
            else if (position.X > destination.X)
                move(1, gameTime);
            if (position.Y < destination.Y)
                move(2, gameTime);
            else if (position.Y > destination.Y)
                move(3, gameTime);
            checkDirection();
            updateAggroBox();
            updateFrame(gameTime);
        }

        public void Draw(SpriteBatch sprite)
        {
            int width = texture.Width / 2;

            Rectangle sourceRectangle = new Rectangle(width * currentFrame, 0, width, texture.Height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            sprite.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        private void move(int id, GameTime gameTime)
        {
            switch(id)
            {
                case 0: position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                case 1: position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                case 2: position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                case 3: position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; break;
                default: break;
            }
        }

        private void checkDirection()
        {
            if (position.X > destination.X - 5 && position.X < destination.X + 5 &&
                position.Y > destination.Y - 5 && position.Y < destination.Y + 5)
            {
                if (destination == firstEnd)
                    destination = secondEnd;
                else if (destination == secondEnd)
                    destination = firstEnd;
            }
        }

        private void updateAggroBox()
        {
            aggroBox.Min = new Vector3(position.X, position.Y, 0);
            aggroBox.Max = new Vector3(position.X + 32, position.Y + 32, 0);
        }

        private void updateFrame(GameTime gameTime)
        {
            frameTime += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds);
            if (frameTime > 500)
            {
                currentFrame++;
                frameTime = 0;
            }
            if (currentFrame >= 2)
                currentFrame = 0;
        }
    }
}
