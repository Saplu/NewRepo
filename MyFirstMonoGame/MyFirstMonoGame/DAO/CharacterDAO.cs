using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMonoGame
{
    public class CharacterDAO
    {
        DbEntities db = new DbEntities();

        public List<Player> GetSurvivors(List<string> survivors)
        {
            var survive = new List<Player>();
            foreach (var thing in db.Player)
            {
                if (survivors.Exists(x => x == thing.Id))
                    survive.Add(thing);
            }
            return survive;
        }

        public List<Player> GetPlayers(List<string> players)
        {
            var plays = new List<Player>();
            foreach (var thing in db.Player)
            {
                if (players.Exists(x => x == thing.Id))
                    plays.Add(thing);
            }
            return plays;
        }

        public List<Player> GetPlayers()
        {
            return db.Player.ToList();
        }

        public void ModifyXp(List<Player> gainers, int xp)
        {
            foreach (var player in gainers)
            {
                player.Xp += xp;
                checkForLevelup(player);
            }
            db.SaveChanges();
        }

        public string TooltipInfo(string name)
        {
            var player = db.Player.ToList().Find(x => x.Id == name);
            return info(player);
        }

        public string TooltipInfo(int classValue)
        {
            return info(classValue);
        }

        public void NameExists(string name)
        {
            if (GetPlayers().Exists(x => x.Id == name))
                throw new Exception("Name not available. Try something more unique");
        }

        public void NewPlayer(int classValue, string name)
        {
            var player = new Player();
            player.Class = classValue;
            player.Id = name;
            var stats = setStats(classValue);
            player.Health = stats[0];
            player.Strength = stats[1];
            player.SpellPower = stats[2];
            player.Level = 1;
            player.Xp = 0;
            player.Crit = 0;
            player.Armor = 0;
            db.Player.Add(player);
            db.SaveChanges();
        }

        private List<int> setStats(int classValue)
        {
            var value = new List<int>();
            switch (classValue)
            {
                case 0: value = new List<int>() { 100, 10, 0 }; break;
                case 1: value = new List<int>() { 80, 5, 20 }; break;
                case 2: value = new List<int>() { 85, 5, 20 }; break;
                case 3: value = new List<int>() { 120, 8, 0 }; break;
                case 4: value = new List<int>() { 80, 5, 20 }; break;
                case 5: value = new List<int>() { 90, 5, 20 }; break;
                case 6: value = new List<int>() { 95, 10, 0 }; break;
                case 7: value = new List<int>() { 120, 5, 15 }; break;
            }
            return value;
        }

        private void checkForLevelup(Player player)
        {
            if (player.Xp >= (player.Level * 100))
            {
                player.Xp -= (player.Level * 100);
                player.Level++;
                var info = player.Class;

                switch (info)
                {
                    case 0: modifyPlayer(player, 20, 2, 0); break;
                    case 1: modifyPlayer(player, 15, 0, 2); break;
                    case 2: modifyPlayer(player, 17, 0, 2); break;
                    case 3: modifyPlayer(player, 24, 2, 0); break;
                    case 4: modifyPlayer(player, 15, 0, 2); break;
                    case 5: modifyPlayer(player, 18, 0, 2); break;
                    case 6: modifyPlayer(player, 17, 2, 0); break;
                    case 7: modifyPlayer(player, 24, 0, 2); break;
                }
            }
        }

        private void modifyPlayer(Player player, int health, int strength, int spellPower)
        {
            player.Health += health;
            player.Strength += strength;
            player.SpellPower += spellPower;
            db.SaveChanges();
        }

        private string info(Player player)
        {
            var value = "";
            switch (player.Class)
            {
                case 0: value = "Warrior. Level: " + player.Level + " Tough fighter with high dmg single-target abilities."; break;
                case 1: value = "Mage. Level: " + player.Level + " Fragile caster with both high single-target nuke and AoE."; break;
                case 2: value = "Blood Priest. Level: " + player.Level + " Single target heals, self survival and debuffing attacks."; break;
                case 3:
                    value = "Protector. Level: " + player.Level + " Real bosstanker. " +
                    "Taunt, threat abilities and permanently reduced taken dmg."; break;
                case 4: value = "Fairy. Level: " + player.Level + " Fragile healer with HoT, shield and party buff."; break;
                case 5:
                    value = "Shaman. Level: " + player.Level + " Tough caster with both single target and aoe nukes. " +
                    "More of a tank, less of a mage."; break;
                case 6:
                    value = "Rogue. Level: " + player.Level + " Tricky fighter. Abilities cost energy, apply poison to the target " +
                    "and gain combo points consumed by ultimate."; break;
                case 7:
                    value = "Templar. Level: " + player.Level + " Magetank. Permanently reduced taken damage. " +
                    "High threat aoe abilities, only one taunt every now and then."; break;
            }
            return value;
        }

        private string info(int classValue)
        {
            var value = "";
            switch (classValue)
            {
                case 0: value = "Warrior. Tough fighter with high dmg single-target abilities."; break;
                case 1: value = "Mage. Fragile caster with both high single-target nuke and AoE."; break;
                case 2: value = "Blood Priest. Single target heals, self survival and debuffing attacks."; break;
                case 3:
                    value = "Protector. Real bosstanker. " +
                "Taunt, threat abilities and permanently reduced taken dmg."; break;
                case 4: value = "Fairy. Fragile healer with HoT, shield and party buff."; break;
                case 5:
                    value = "Shaman. Tough caster with both single target and aoe nukes. " +
                "More of a tank, less of a mage."; break;
                case 6:
                    value = "Rogue. Tricky fighter. Abilities cost energy, apply poison to the target " +
                "and gain combo points consumed by ultimate."; break;
                case 7:
                    value = "Templar. Magetank. Permanently reduced taken damage. " +
                "High threat aoe abilities, but no taunt."; break;
            }
            return value;
        }
    }
}
