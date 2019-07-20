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
    public class Attack
    {
        List<Vector2> positions;
        Texture2D texture;
        int columns, currentFrame, attackTime;
        bool attackRunning;

        public List<Vector2> Positions { get => positions; set => positions = value; }
        public bool AttackRunning { get => attackRunning; set => attackRunning = value; }

        public Attack(Texture2D textureAtlas)
        {
            positions = new List<Vector2>();
            texture = textureAtlas;
            columns = 3;
            currentFrame = 0;
            attackTime = 0;
        }


        public void Update(GameTime time)
        {
            if (attackRunning)
            {
                attackTime += Convert.ToInt32(time.ElapsedGameTime.TotalMilliseconds);
                if (attackTime < 150)
                    currentFrame = 0;
                else if (attackTime < 300)
                    currentFrame = 1;
                else if (attackTime < 450)
                    currentFrame = 2;
                else if (attackTime < 600)
                    currentFrame = 1;
                else if (attackTime >= 700)
                {
                    attackTime = 0;
                    currentFrame = 0;
                    attackRunning = false;
                    positions.Clear();
                }
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            if (attackRunning)
            {
                int width = texture.Width / columns;
                int column = currentFrame % columns;

                var sourceRectangle = new Rectangle(width * column, 0, width, texture.Height);

                foreach (var position in positions)
                {
                    var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 80, 60);

                    var destination = new Vector2(destinationRectangle.X, destinationRectangle.Y);
                    var origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

                    sprite.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
                }
            }
        }
    }
}
