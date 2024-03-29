﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Party
    {
        private List<Player> players;
        private int money, map, side;

        public List<Player> Players { get => players; set => players = value; }
        public int Money { get => money; set => money = value; }
        public int Map { get => map; set => map = value; }
        public int Side { get => side; set => side = value; }

        public Party(List<Player> players)
        {
            this.players = players;
            Money = 0;
            map = 0;
            side = 0;
        }
    }
}
