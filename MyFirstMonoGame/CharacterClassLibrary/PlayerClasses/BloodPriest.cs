using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.BloodPriest;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class BloodPriest : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public BloodPriest(string name)
        {
            Health = 85;
            MaxHealth = Health;
            Strength = 0;
            SpellPower = 15;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Name = name;
            ClassName = Enums.ClassName.BloodPriest;
            Items = new List<Item>();
            ItemTypes = new List<Enums.ItemType> { Enums.ItemType.Cloth, Enums.ItemType.Leather };
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            ItemPlaces.Add(Enums.ItemPlace.OffHand);
        }

        private int lifeLeech()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var leech = new LifeLeech();
            Cooldowns[0] = leech.Cooldown;
            //RecieveHeal(leech.Action(SpellPower, Crit, multi, increase));
            return leech.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var leech = new LifeLeech();
            return leech.Info();
        }

        private int heal()
        {
            var multi = 1;
            var increase = 0;
            var heal = new Heal();
            Cooldowns[1] = heal.Cooldown;
            return heal.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var heal = new Heal();
            return heal.Info();
        }

        private int weakenBlood()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var weak = new WeakenBlood();
            Cooldowns[2] = weak.Cooldown;
            return weak.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var weak = new WeakenBlood();
            return weak.Info();
        }

        private int sacrifice()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var sacrifice = new Sacrifice();
            Cooldowns[3] = sacrifice.Cooldown;
            Health -= (Health / 10);
            return sacrifice.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var sacrifice = new Sacrifice();
            return sacrifice.Info();
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Life Leech": return lifeLeech();
                case "Heal": return heal();
                case "Weaken Blood": return weakenBlood();
                case "Sacrifice": return sacrifice();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            return 1;
        }

        public override string setPic()
        {
            return "BloodPriest";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();
            switch(id)
            {
                case "Life Leech": list = util.setTargets(1, 4); return list;
                case "Weaken Blood": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Sacrifice": list = util.setTargets(targetPosition, 1); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch(id)
            {
                case "Life Leech": return Convert.ToInt32(lifeLeech() / 3);
                case "Weaken Blood": return Convert.ToInt32(SpellPower * .4);
                case "Sacrifice": return .7;
                default: return 0;
            }
        }

        public override int GetThreat(string id)
        {
            switch(id)
            {
                case "Life Leech": return Convert.ToInt32(SpellPower * .8);
                case "Heal": return Convert.ToInt32(SpellPower * .3);
                case "Weaken Blood": return Convert.ToInt32(SpellPower * .8);
                case "Sacrifice": return Convert.ToInt32(SpellPower * .4);
                default: return 0;
            }
        }
    }
}
