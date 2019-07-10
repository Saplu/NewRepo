using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Rogue;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Rogue : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        private int energy;
        private const int maxEnergy = 100;
        private int comboPoints;
        private const int maxCombo = 5;
        private int lastCombo;

        public int Energy { get => energy; set => energy = value; }
        public int ComboPoints { get => comboPoints; set => comboPoints = value; }
        public static int MaxCombo => maxCombo;
        public static int MaxEnergy => maxEnergy;

        public Rogue(string name)
        {
            Health = 95;
            MaxHealth = Health;
            Strength = 15;
            Crit = 0;
            SpellPower = 0;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Name = name;
            ClassName = Enums.ClassName.Rogue;
            Items = new List<Item>();
            ItemTypes = new List<Enums.ItemType>() { Enums.ItemType.Cloth, Enums.ItemType.Leather };
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 0 };
            energy = maxEnergy;
            comboPoints = 0;
        }

        private int stab()
        {
            manageEnergy(0, 40);
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var stab = new Stab();
            Cooldowns[0] = stab.Cooldown;
            addCombo(1);
            return stab.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var stab = new Stab();
            return stab.Info();
        }

        private int mutilate()
        {
            manageEnergy(60, 20);
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var mutilate = new Mutilate();
            Cooldowns[1] = mutilate.Cooldown;
            addCombo(2);
            return mutilate.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var mutilate = new Mutilate();
            return mutilate.Info();
        }

        private int jawbreaker()
        {
            manageEnergy(50, 20);
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var jaw = new Jawbreaker();
            Cooldowns[2] = jaw.Cooldown;
            addCombo(1);
            return jaw.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var jaw = new Jawbreaker();
            return jaw.Info();
        }

        private int bladeFlurry()
        {
            if (ComboPoints == 0)
                throw new Exception("Cannot use without combo points.");
            manageEnergy(40, 20);
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var flurry = new BladeFlurry();
            lastCombo = ComboPoints;
            Cooldowns[3] = flurry.Cooldown;
            ComboPoints = 0;
            return flurry.Action(Strength, Crit, multi, increase, lastCombo);
        }

        public override string[] Ability4()
        {
            var flurry = new BladeFlurry();
            return flurry.Info();
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Stab": return stab();
                case "Mutilate": return mutilate();
                case "Jawbreaker": return jawbreaker();
                case "Blade Flurry": return bladeFlurry();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Stab": return 1;
                case "Mutilate": return 1;
                case "Jawbreaker": return 1;
                case "Blade Flurry": return 1;
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "rogue";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            var util = new Utils.TargetSetter();
            switch(id)
            {
                case "Stab": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Mutilate": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Jawbreaker": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Blade Flurry": if (lastCombo == 5) list = util.setTargets(targetPosition, 4, enemyCount);
                    else list = util.setTargets(targetPosition, 1, enemyCount); return list;
            }
            return list;
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            var stab = new Stab();
            var poison = stab.Poison(Strength);
            switch(id)
            {
                case "Stab": return poison;
                case "Mutilate": return poison;
                case "Jawbreaker": return poison;
                case "Blade Flurry": return poison;
                default: return 0;
            }
        }

        public override string ToString()
        {
            var statuses = getStatuses();
            return "Health: " + Health + "/" + MaxHealth + "<br/>" +  ComboPoints + " " + Energy + " / " + MaxEnergy + statuses;
        }

        private void addCombo(int value)
        {
            if (ComboPoints < MaxCombo)
                ComboPoints += value;
            if (ComboPoints > MaxCombo)
                ComboPoints = 5;
        }

        private void manageEnergy(int cost, int after)
        {
            if (cost > Energy)
                throw new Exception("Not enough energy.");
            else Energy -= cost;
            if (Energy + after > MaxEnergy)
                Energy = MaxEnergy;
            else Energy += after;
        }

        public override void AfterCombatReset()
        {
            Health = MaxHealth;
            Cooldowns = new int[4] { 0, 0, 0, 0 };
            Energy = 100;
            ComboPoints = 0;
            Statuses = new List<CombatLogicClassLibrary.Status>();
        }
    }
}
