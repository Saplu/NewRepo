using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstMonoGame.Presentation
{
    public class TextBox
    {
        string text;
        int x, y, width, height;
        SpriteFont font;
        bool selected;
        MouseState previousState, currentState;
        KeyboardState previous, current;
        TextHandler textHandler;

        public string Text { get => text; set => text = value; }

        public TextBox(SpriteFont font, int x, int y, int width, int height)
        {
            text = "";
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.font = font;
            textHandler = new TextHandler();
            selected = false;
        }

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            updateStates(mouse, keyboard);
            if (selected == true) updateText();
        }

        public void Clicked()
        {
            if (currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                manageClick();
            }
        }

        public void Draw(SpriteBatch sprite, SpriteFont font, Color color)
        {
            sprite.DrawString(font, text, new Vector2(x, y), color);
        }

        private void manageClick()
        {
            if (currentState.X >= x && currentState.X <= x + width &&
                currentState.Y >= y && currentState.Y <= y + height)
                selected = true;
            else selected = false;
        }

        private void updateStates(MouseState mouse, KeyboardState keyboard)
        {
            previousState = currentState;
            currentState = mouse;
            previous = current;
            current = keyboard;
        }

        private void updateText()
        {
            text += textHandler.Update(current);
            text = textHandler.BackSpace(current, text);
        }
    }
}
