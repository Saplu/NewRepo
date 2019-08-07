using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMonoGame
{
    public class TextHandler
    {
        Keys[] oldState, newState;

        public string Update(KeyboardState state)
        {
            var value = "";
            newState = state.GetPressedKeys();

            if (newState.Length > 0)
                value = getText();

            oldState = newState;
            return value;
        }

        public string BackSpace(KeyboardState current, string text)
        {
            if (current.IsKeyDown(Keys.Back) && text.Length > 0)
                text = text.Remove(text.Count() - 1);
            return text;
        }

        private string getText()
        {
            var pressedList = new List<char>();

            foreach(var item in newState)
            {
                if (!oldState.Contains(item))
                    pressedList.Add(addChar(item));
            }
            var value = new string(pressedList.ToArray());
            value = value.Replace(" ", "");
            return value;
        }

        private char addChar(Keys item)
        {
            switch(item)
            {
                case Keys.Q: return 'Q';
                case Keys.W: return 'W';
                case Keys.E: return 'E';
                case Keys.R: return 'R';
                case Keys.T: return 'T';
                case Keys.Y: return 'Y';
                case Keys.U: return 'U';
                case Keys.I: return 'I';
                case Keys.O: return 'O';
                case Keys.P: return 'P';
                case Keys.A: return 'A';
                case Keys.S: return 'S';
                case Keys.D: return 'D';
                case Keys.F: return 'F';
                case Keys.G: return 'G';
                case Keys.H: return 'H';
                case Keys.J: return 'J';
                case Keys.K: return 'K';
                case Keys.L: return 'L';
                case Keys.Z: return 'Z';
                case Keys.X: return 'X';
                case Keys.C: return 'C';
                case Keys.V: return 'V';
                case Keys.B: return 'B';
                case Keys.N: return 'N';
                case Keys.M: return 'M';
                default: return ' ';
            }
        }
    }
}
