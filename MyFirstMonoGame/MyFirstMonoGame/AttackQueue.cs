using Microsoft.Xna.Framework;
using MyFirstMonoGame.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMonoGame
{
    public class AttackQueue
    {
        private List<Attack> attacks;
        private List<List<int>> healths;

        public List<Attack> Attacks { get => attacks; set => attacks = value; }
        public List<List<int>> Healths { get => healths; set => healths = value; }

        public AttackQueue()
        {
            attacks = new List<Attack>();
            healths = new List<List<int>>();
        }

        public void Add(Attack attack, List<int> list)
        {
            attacks.Add(attack);
            healths.Add(list);
        }

        public void Update(GameTime gameTime)
        {
            if (attacks.Count > 0)
            {
                attacks[0].Update(gameTime);
                if (attacks[0].Finished)
                {
                    attacks.RemoveAt(0);
                    healths.RemoveAt(0);
                }
            }
        }
    }
}
