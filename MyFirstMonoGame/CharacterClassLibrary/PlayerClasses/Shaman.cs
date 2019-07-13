using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Shaman;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Shaman : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Shaman(string name)
        {
            Health = 90;
            MaxHealth = Health;
            Strength = 0;
            Crit = 0;
            SpellPower = 15;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Name = name;
            ClassName = Enums.ClassName.Shaman;
            Items = new List<Item>();
            ItemTypes = new List<Enums.ItemType>() { Enums.ItemType.Cloth, Enums.ItemType.Leather, Enums.ItemType.Mail };
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            ItemPlaces.Add(Enums.ItemPlace.Shield);
        }

        private int lightningBolt()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var light = new LightningBolt();
            Cooldowns[0] = light.Cooldown;
            return light.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var light = new LightningBolt();
            return light.Info();
        }

        private int chainLightning()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var chain = new ChainLightning();
            Cooldowns[1] = chain.Cooldown;
            return chain.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var chain = new ChainLightning();
            return chain.Info();
        }

        private int flameShock()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var flame = new FlameShock();
            Cooldowns[2] = flame.Cooldown;
            return flame.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var flame = new FlameShock();
            return flame.Info();
        }

        private int thunder()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var thunder = new Thunder();
            Cooldowns[3] = thunder.Cooldown;
            return thunder.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var thunder = new Thunder();
            return thunder.Info();
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Lightning Bolt": return lightningBolt();
                case "Chain Lightning": return chainLightning();
                case "Flame Shock": return flameShock();
                case "Thunder": return thunder();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Lightning Bolt": return 1;
                case "Chain Lightning": return 3;
                case "Flame Shock": return 1;
                case "Thunder": return 4;
                default: return 1;
            }
        }


        public override string setPic()
        {
            return "shamaani";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();
            if (id == "Flame Shock")
                list = util.setTargets(targetPosition, 1, enemyCount);
            if (id == "Thunder")
                list = util.setTargets(targetPosition, 1, enemyCount);
            return list;
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch(id)
            {
                case "Flame Shock": return (flameShock() / 3);
                case "Thunder": return 1;
                default: return 0;
            }
        }
    }
}
