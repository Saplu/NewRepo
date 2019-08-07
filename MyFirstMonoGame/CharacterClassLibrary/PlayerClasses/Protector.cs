using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Protector;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Protector : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Protector(string name)
        {
            Health = 120;
            MaxHealth = Health;
            Strength = 12;
            Crit = 0;
            SpellPower = 0;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Items = new List<Item>(){new Item(0, Enums.ItemPlace.MainHand), new Item(0, Enums.ItemPlace.OffHand),
                new Item(3, Enums.ItemPlace.Head), new Item(3, Enums.ItemPlace.Chest), new Item(3, Enums.ItemPlace.Hands),
                new Item(3, Enums.ItemPlace.Legs), new Item(3, Enums.ItemPlace.Feet), new Item(5, Enums.ItemPlace.Shield)};
            Name = name;
            ClassName = Enums.ClassName.Protector;
            ItemTypes = new List<Enums.ItemType>
            { Enums.ItemType.Cloth, Enums.ItemType.Leather, Enums.ItemType.Mail, Enums.ItemType.Plate };
            var stat = new CombatLogicClassLibrary.Statuses.TakenDmgMultiplier(Int32.MaxValue, new List<int>(), .85);
            Statuses = new List<CombatLogicClassLibrary.Status>() { stat };
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            ItemPlaces.Add(Enums.ItemPlace.Shield);
        }

        private int tauntingBlow()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var taunt = new TauntingBlow();
            Cooldowns[0] = taunt.Cooldown;
            return taunt.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var taunt = new TauntingBlow();
            return taunt.Info();
        }

        private int devastatingStrike()
        {
            var increase = getAttackModifier();
            var multi = getAttackMultiplier();
            var devastate = new DevastatingStrike();
            Cooldowns[1] = devastate.Cooldown;
            return devastate.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var devastate = new DevastatingStrike();
            return devastate.Info();
        }

        private int sweep()
        {
            var increase = getAttackModifier();
            var multi = getAttackMultiplier();
            var sweep = new Sweep();
            Cooldowns[2] = sweep.Cooldown;
            return sweep.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var sweep = new Sweep();
            return sweep.Info();
        }

        private int challengingShout()
        {
            var increase = getAttackModifier();
            var multi = getAttackMultiplier();
            var challenge = new ChallengingShout();
            Cooldowns[3] = challenge.Cooldown;
            Statuses.Add(challenge.applySelfStatus());
            return challenge.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var challenge = new ChallengingShout();
            return challenge.Info();
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Taunting Blow": return tauntingBlow();
                case "Devastating Strike": return devastatingStrike();
                case "Sweep": return sweep();
                case "Challenging Shout": return challengingShout();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            switch (id)
            {
                case "Taunting Blow": return 1;
                case "Devastating Strike": return 1;
                case "Sweep": return 4;
                case "Challenging Shout": return 4;
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "Tankki";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var util = new Utils.TargetSetter();
            var list = new List<int>();
            switch (id)
            {
                case "Taunting Blow": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Challenging Shout": list = util.setTargets(targetPosition, 4, enemyCount); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch (id)
            {
                case "Taunting Blow": return this.Position;
                case "Challenging Shout": return this.Position;
                default: return 0;
            }
        }

        public override int GetThreat(string id)
        {
            switch (id)
            {
                case "Taunting Blow": return Convert.ToInt32(Strength * 2.4);
                case "Devastating Strike": return Strength * 3;
                case "Sweep": return Convert.ToInt32(Strength * 1.4);
                case "Challenging Shout": return Convert.ToInt32(Strength * 1.5);
                default: return 0;
            }
        }
        public override void AfterCombatReset()
        {
            Health = MaxHealth;
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            var stat = new CombatLogicClassLibrary.Statuses.TakenDmgMultiplier(Int32.MaxValue, new List<int>(), .85);
            Statuses = new List<CombatLogicClassLibrary.Status>() { stat };
        }
    }
}
