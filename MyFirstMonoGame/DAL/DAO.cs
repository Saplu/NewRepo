using System;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class DAO
    {
        private string filePath;
        private List<Player> players;
        private List<Party> parties;
        private List<int> playerLines;

        public List<Player> Players { get => players; set => players = value; }

        public DAO(int partyNumber)
        {
            string fileName = "party" + partyNumber + ".txt";
            filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            players = new List<Player>();
            parties = new List<Party>();
            playerLines = new List<int>();
        }

        public void Update(Party party)
        {
            var lines = readFile();
            var newLines = new List<string>();
            separatePlayers(lines);
            var partyPlayerLines = new List<int>();

            foreach (var player in party.Players)
            {
                var startLine = findLine(player.Name, lines);
                partyPlayerLines.Add(startLine);
            }
            foreach (var item in partyPlayerLines)
            {
                newLines.AddRange(rewriteData(item, party, lines, partyPlayerLines.IndexOf(item)));
            }
            updateFile(newLines);
            //var targetParty = findParty(party);
        }

        public void Read()
        {
            var lines = readFile();

            separatePlayers(lines);
        }

        private List<string> readFile()
        {
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
        /*
        private void separateParties(List<string> lines)
        {
            separatePlayers(lines);
            for (int i = 0; i < players.Count; i+=4)
            {
                var list = new List<Player>();
                list.Add(players[i]);
                list.Add(players[i + 1]);
                list.Add(players[i + 2]);
                list.Add(players[i + 3]);
                var party = new Party(list);
                parties.Add(party);
            }
        }
        */
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
            foreach (var item in playerLines)
            {
                var data = split(item + 1, lines);
                var items = generateItems(item, lines, data[0]);
                var player = new Player(data[0], Convert.ToInt32(data[4]), Convert.ToInt32(data[8]),
                    Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3]),
                    Convert.ToInt32(data[5]), Convert.ToInt32(data[6]), Convert.ToInt32(data[4]),
                    items, getItemTypes(Convert.ToInt32(data[8])));
                players.Add(player);
            }
        }


        private string[] split(int thing, List<string> lines)
        {
            string[] data = lines[thing].Split(',');
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
                    Convert.ToInt32(data[7]), Convert.ToInt32(data[9]), Convert.ToInt32(data[4]), data[0],
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
        /*
        private Party findParty(Party party)
        {
            foreach(var item in parties)
            {
                var nameList = new List<string>();
                nameList.Add(item.Players[0].Name);
                nameList.Add(item.Players[1].Name);
                nameList.Add(item.Players[2].Name);
                nameList.Add(item.Players[3].Name);
                if (nameList.Contains(party.Players[0].Name) && nameList.Contains(party.Players[1].Name) &&
                    nameList.Contains(party.Players[2].Name) && nameList.Contains(party.Players[3].Name))
                    return item;
            }
            throw new Exception("No party found. Weird.");
        }
        */
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
            string updated = original.Replace(original, party.Players[index].ToString());
            value.Add(updated);
            for (int i = 2; i < 10; i++)
            {
                string origin = lines[item + i];
                string update = origin.Replace(origin, party.Players[index].Items[i - 2].ToString());
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

