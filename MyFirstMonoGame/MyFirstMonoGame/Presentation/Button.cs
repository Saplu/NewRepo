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
    public class Button
    {
        private int buttonX, buttonY, buttonWidth, buttonHeight;
        private Texture2D texture;
        private string text;
        private Vector2 center;
        MouseState previousState, currentState;
        int x, y;

        public int ButtonX { get => buttonX; }
        public int ButtonY { get => buttonY; }
        public int ButtonWidth { get => buttonWidth; set => buttonWidth = value; }
        public int ButtonHeight { get => buttonHeight; set => buttonHeight = value; }
        public string Text { get => text; set => text = value; }

        public Texture2D Texture { get => texture; set => texture = value; }

        public Button(Texture2D texture, int buttonX, int buttonY, string text, int width, int height)
        {
            this.texture = texture;
            this.buttonX = buttonX;
            this.buttonY = buttonY;
            this.text = text;
            this.buttonWidth = width;
            this.buttonHeight = height;
            center = new Vector2(buttonX + (buttonWidth / 2), buttonY + (buttonHeight / 2));
        }

        public bool ButtonClicked()
        {
            x = currentState.X;
            y = currentState.Y;

            if (currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                previousState = currentState;
                if (x < buttonX + buttonWidth && x > buttonX &&
                    y < buttonY + buttonHeight && y > buttonY)
                    return true;
                else return false;
            }
            return false;
        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            var textLength = font.MeasureString(text);
            var textPosition = new Vector2(center.X - (textLength.X / 2), center.Y - (textLength.Y / 2));

            sprite.Draw(texture, new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), Color.White);
            sprite.DrawString(font, text, textPosition, Color.BurlyWood);
        }

        public void UpdateMouseState(MouseState current)
        {
            previousState = currentState;
            currentState = current;
        }

        public bool MouseOver()
        {
            x = currentState.X;
            y = currentState.Y;

            if (x < buttonX + buttonWidth && x > buttonX &&
                y < buttonY + buttonHeight && y > buttonY)
                return true;
            else return false;
        }
    }
}
