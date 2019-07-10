using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Party
    {
        private List<Player> players;
        private int money;

        public List<Player> Players { get => players; set => players = value; }
        public int Money { get => money; set => money = value; }

        public Party(List<Player> players)
        {
            this.players = players;
            Money = 0;
        }
    }
}
