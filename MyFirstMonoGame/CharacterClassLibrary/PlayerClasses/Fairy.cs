using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Fairy;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Fairy : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Fairy(string name)
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
            ClassName = Enums.ClassName.Fairy;
            Items = new List<Item>();
            ItemTypes = new List<Enums.ItemType>() { Enums.ItemType.Cloth };
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            ItemPlaces.Add(Enums.ItemPlace.OffHand);
        }

        private int laser()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var laser = new Laser();
            Cooldowns[0] = laser.Cooldown;
            return laser.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var laser = new Laser();
            return laser.Info();
        }

        private int bubble()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var bubble = new Bubble();
            Cooldowns[1] = bubble.Cooldown;
            return bubble.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var bubble = new Bubble();
            return bubble.Info();
        }

        private int healingWords()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var heal = new HealingWords();
            Cooldowns[2] = heal.Cooldown;
            return heal.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var heal = new HealingWords();
            return heal.Info();
        }

        private int inspire()
        {
            var inspire = new Inspire();
            Cooldowns[3] = inspire.Cooldown;
            return inspire.Action(SpellPower);
        }

        public override string[] Ability4()
        {
            var inspire = new Inspire();
            return inspire.Info();
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Laser": return laser();
                case "Bubble": return bubble();
                case "Healing Words": return healingWords();
                case "Inspire": return inspire();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            switch (id)
            {
                case "Laser": return 1;
                case "Bubble": return 1;
                case "Healing Words": return 1;
                case "Inspire": return 4;
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "keijju";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();

            switch(id)
            {
                case "Laser": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Bubble": list = util.setTargets(targetPosition, 1); return list;
                case "Healing Words": list = util.setTargets(targetPosition, 1); return list;
                case "Inspire": list = util.setTargets(targetPosition, 4); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch(id)
            {
                case "Laser": return .9;
                case "Bubble": return bubble();
                case "Healing Words": return healingWords();
                case "Inspire": return inspire();
                default: return 0;
            }
        }
    }
}
