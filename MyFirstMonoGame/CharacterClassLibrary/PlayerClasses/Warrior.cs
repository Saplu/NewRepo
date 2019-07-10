using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Warrior;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Warrior : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Warrior(string name)
        {
            Health = 100;
            MaxHealth = Health;
            Strength = 15;
            Crit = 0;
            SpellPower = 0;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Items = new List<Item>();
            Name = name;
            ClassName = Enums.ClassName.Warrior;
            ItemTypes = new List<Enums.ItemType>
            { Enums.ItemType.Cloth, Enums.ItemType.Leather, Enums.ItemType.Mail};
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 4 };
        }

        private int attack()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var attack = new Attack();
            Cooldowns[0] = attack.Cooldown;
            return attack.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var attack = new Attack();
            return attack.Info();
        }

        private int viciousBlow()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var vicious = new ViciousBlow();
            Cooldowns[1] = vicious.Cooldown;
            return vicious.Action(Strength, multi, increase);
        }

        public override string[] Ability2()
        {
            var vicious = new ViciousBlow();
            return vicious.Info();
        }

        private int battleCry()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var cry = new BattleCry();
            Cooldowns[2] = cry.Cooldown;
            return cry.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var cry = new BattleCry();
            return cry.Info();
        }

        private int execute()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var exe = new Execute();
            Cooldowns[3] = exe.Cooldown;
            return exe.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var exe = new Execute();
            return exe.Info();
        }

        public override int UseAbility(string id)
        {
            if (id == "Attack")
                return attack();
            else if (id == "Vicious Blow")
                return viciousBlow();
            else if (id == "Battle Cry")
                return battleCry();
            else if (id == "Execute")
                return execute();
            else return 1;
        }

        public override int GetTargets(string id)
        {
            if (id == "Attack")
                return 1;
            else if (id == "Vicious Blow")
                return 1;
            else if (id == "Battle Cry")
                return 4;
            else if (id == "Execute")
                return 1;
            else return 1;
        }

        public override string setPic()
        {
            return "ninja";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            if (id == "Battle Cry")
            {
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
            }
            return list;
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            if (id == "Battle Cry")
                return 1.2;
            else return 0;
        }
    }
}
