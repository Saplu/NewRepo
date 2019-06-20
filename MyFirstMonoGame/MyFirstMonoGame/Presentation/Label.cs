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
    public class Label
    {
        private string id;
        private string text;
        private int x, y, width, height;
        private SpriteFont font;

        public string Text { get => text; set => text = value; }

        public Label(string id,SpriteFont font, string text, int positionX, int positionY, int width, int height)
        {
            this.id = id;
            this.text = text;
            this.x = positionX;
            this.y = positionY;
            this.width = width;
            this.height = height;
            this.font = font;

            this.text = trimTextLength();
        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.DrawString(font, text, new Vector2(x, y), Color.Black);
        }

        public void Update(string newText)
        {
            if (newText != text)
            {
                text = newText;
                text = trimTextLength();
            }
        }

        private string trimTextLength()
        {
            var splitter = new string[] { "\r\n" };
            var lines = text.Split(splitter, StringSplitOptions.None);
            var value = stringSplitter(lines);

            return value;
        }

        private string stringSplitter(string[] split)
        {
            var value = "";
            foreach (var line in split)
            {
                var length = font.MeasureString(line).X;
                if (length > width)
                {
                    var newLine = "";
                    var newLineLength = font.MeasureString(newLine).X;
                    for (int i = 0; newLineLength < width; i++)
                    {
                        newLine += line[i];
                        newLineLength = font.MeasureString(newLine).X;
                    }
                    value += newLine + "\r\n";
                }
                else value += line + "\r\n";
            }
            return value;
        }
    }
}
