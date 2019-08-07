using System;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class DAO
    {
        private string filePath;
        private List<Player> players;
        private Party party;
        private List<int> playerLines, partyPlayerLines;
        private int number;

        public List<Player> Players { get => players; set => players = value; }
        public Party Party { get => party; set => party = value; }
        public int Number { get => number; set => number = value; }

        public DAO(int partyNumber)
        {
            number = partyNumber;
            string fileName = "party" + number + ".txt";
            filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            players = new List<Player>();
            party = new Party(players);
            playerLines = new List<int>();
            partyPlayerLines = new List<int>(){ 0, 10, 20, 30};
        }

        public void Update(Party party)
        {
            var lines = readFile();
            var newLines = new List<string>();
            separatePlayers(lines);
            /*var partyPlayerLines = new List<int>();
            
            foreach (var player in party.Players)
            {
                var startLine = findLine(player.Name, lines);
                partyPlayerLines.Add(startLine);
            }*/
            
            foreach (var item in partyPlayerLines)
            {
                newLines.AddRange(rewriteData(item, party, lines, partyPlayerLines.IndexOf(item)));
            }
            newLines.Add(party.Money.ToString() + "." + party.Map.ToString() + "." + party.Side.ToString());
            updateFile(newLines);
        }

        public void Read()
        {
            var lines = readFile();
            separatePlayers(lines);
            party = new Party(players);
            var last = split(lines.Count - 1, lines);
            party.Money = Convert.ToInt32(last[0]);
            party.Map = Convert.ToInt32(last[1]);
            party.Side = Convert.ToInt32(last[2]);
        }

        private List<string> readFile()
        {
            string fileName = "party" + number + ".txt";
            filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            var lines = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private void separatePlayers(List<string> lines)
        {
            playerLines = new List<int>();
            foreach (var line in lines)
            {
                if (line == "PlayerBegins" + playerLines.Count)
                {
                    playerLines.Add(lines.IndexOf(line));
                }
            }
            generatePlayers(lines, playerLines);
        }

        private void generatePlayers(List<string> lines, List<int> playerLines)
        {
            players = new List<Player>();
            foreach (var item in playerLines)
            {
                var data = split(item + 1, lines);
                var items = generateItems(item, lines, data[0]);
                var player = new Player(data[0], Convert.ToInt32(data[7]), Convert.ToInt32(data[8]),
                    Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3]),
                    Convert.ToInt32(data[5]), Convert.ToInt32(data[6]), Convert.ToDouble(data[4]),
                    items, getItemTypes(Convert.ToInt32(data[8])));
                players.Add(player);
            }
        }


        private string[] split(int thing, List<string> lines)
        {
            string[] data = lines[thing].Split('.');
            return data;
        }

        private List<Item> generateItems(int thing, List<string> lines, string name)
        {
            var items = new List<Item>();
            for (int i = 2; i < 10; i++)
            {
                var data = split(thing + i, lines);
                var item = new Item(Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3]),
                    Convert.ToInt32(data[6]), Convert.ToInt32(data[8]), Convert.ToInt32(data[5]),
                    Convert.ToInt32(data[7]), Convert.ToInt32(data[9]), Convert.ToDouble(data[4]), data[0],
                    name);
                items.Add(item);
            }
            return items;
        }

        private List<int> getItemTypes(int className)
        {
            var list = new List<int>() { 0 };
            switch (className)
            {
                case 0: list.Add(1); list.Add(2); return list;
                case 2: list.Add(1); return list;
                case 3: list.Add(1); list.Add(2); list.Add(3); return list;
                case 5: list.Add(1); list.Add(2); return list;
                case 6: list.Add(1); return list;
                case 7: list.Add(1); list.Add(2); list.Add(3); return list;
                default: return list;
            }
        }

        private int findLine(string name, List<string> lines)
        {
            foreach (var item in playerLines)
            {
                var list = new List<string>() { lines[item + 1] };
                var splitted = split(0, list);
                if (splitted[0] == name)
                    return item;
            }
            throw new Exception("No player found. Weird.");
        }

        private List<string> rewriteData(int item, Party party, List<string> lines, int index)
        {
            var value = new List<string>() { "PlayerBegins" + index.ToString() + "\r\n" };
            string original = lines[item + 1];
            string updated = party.Players[index].ToString();
            value.Add(updated);
            for (int i = 2; i < 10; i++)
            {
                string origin = lines[item + i];
                string update = party.Players[index].Items[i - 2].ToString();
                value.Add(update);
            }
            return value;
        }

        private void updateFile(List<string> newLines)
        {
            var value = "";
            foreach (var line in newLines)
                value += line;
            File.WriteAllText(filePath, value);
        }
    }
}

