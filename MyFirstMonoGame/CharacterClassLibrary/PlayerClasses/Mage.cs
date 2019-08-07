using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Mage;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Mage : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Mage(string name)
        {
            Health = 80;
            MaxHealth = Health;
            Strength = 0;
            Crit = 0;
            SpellPower = 15;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Name = name;
            ClassName = Enums.ClassName.Mage;
            Items = new List<Item>(){new Item(0, Enums.ItemPlace.MainHand), new Item(0, Enums.ItemPlace.OffHand),
                new Item(0, Enums.ItemPlace.Head), new Item(0, Enums.ItemPlace.Chest), new Item(0, Enums.ItemPlace.Hands),
                new Item(0, Enums.ItemPlace.Legs), new Item(0, Enums.ItemPlace.Feet), new Item(0, Enums.ItemPlace.Shield)};
            ItemTypes = new List<Enums.ItemType> { Enums.ItemType.Cloth };
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            ItemPlaces.Add(Enums.ItemPlace.OffHand);
        }

        private int fireball()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var fireball = new Fireball();
            Cooldowns[0] = fireball.Cooldown;
            return fireball.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var fireball = new Fireball();
            return fireball.Info();
        }

        private int fireWithin()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var fire = new FireWithin();
            Cooldowns[1] = fire.Cooldown;
            return fire.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var fire = new FireWithin();
            return fire.Info();
        }
        
        private int lavaField()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var lava = new LavaField();
            Cooldowns[2] = lava.Cooldown;
            return lava.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var lava = new LavaField();
            return lava.Info();
        }

        private int hellfire()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var hellfire = new Hellfire();
            Cooldowns[3] = hellfire.Cooldown;
            return hellfire.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var hellfire = new Hellfire();
            return hellfire.Info();
        }

        public override int UseAbility(string id)
        {
            switch (id)
            {
                case "Fireball": return fireball();
                case "Fire Within": return fireWithin();
                case "Lava Field": return lavaField();
                case "Hellfire": return hellfire();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            switch (id)
            {
                case "Fireball": return 1;
                case "Fire Within": return 1;
                case "Lava Field": return 3;
                case "Hellfire": return 4;
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "welho";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            if (id == "Lava Field")
            {
                var util = new Utils.TargetSetter();
                list = util.setTargets(targetPosition, 3, enemyCount);
            }
            if (id == "Hellfire")
            {
                var util = new Utils.TargetSetter();
                list = util.setTargets(targetPosition, 1, enemyCount);
            }
            return list;
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            if (id == "Lava Field")
            {
                var multi = getAttackMultiplier();
                var increase = getAttackModifier();
                var lava = new LavaField();
                return lava.Action(SpellPower, Crit, multi, increase);
            }
            if (id == "Hellfire")
            {
                var multi = getAttackMultiplier();
                var increase = getAttackModifier();
                var hellfire = new Hellfire();
                return hellfire.DoT(SpellPower, Crit, multi, increase);
            }
            else return 0;
        }

        public override int GetThreat(string id)
        {
            switch(id)
            {
                case "Lava Field": return Convert.ToInt32(SpellPower * .8);
                case "Fireball": return SpellPower;
                case "Fire Within": return Convert.ToInt32(SpellPower * .8);
                case "Hellfire": return SpellPower;
                default: return 0;
            }
        }
    }
}
