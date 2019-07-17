using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary
{
    public class Party
    {
        List<Player> players;
        int money, map, side;

        public List<Player> Players { get => players; set => players = value; }
        public int Money { get => money; set => money = value; }
        public int Map { get => map; set => map = value; }
        public int Side { get => side; set => side = value; }

        public Party(List<Player> players, int money, int map, int side)
        {
            this.players = players;
            this.money = money;
            this.map = map;
            this.side = side;
        }
    }
}
