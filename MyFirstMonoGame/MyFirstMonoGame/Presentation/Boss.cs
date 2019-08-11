using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstMonoGame.Presentation
{
    public class Boss : Enemy
    {
        int id;

        public int Id { get => id; set => id = value; }

        public Boss(Texture2D texture, Vector2 position, int id) : base(texture, 0f, position, position, position)
        {
            this.id = id;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
