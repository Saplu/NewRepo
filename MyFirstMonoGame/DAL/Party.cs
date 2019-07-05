using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Party
    {
        private List<Player> players;

        public List<Player> Players { get => players; set => players = value; }

        public Party(List<Player> players)
        {
            this.players = players;
        }
    }
}
