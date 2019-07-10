using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class Character
    {
        private int health;
        private int maxHealth;
        private int strength;
        private double crit;
        private int spellPower;
        private int armor;
        private int level;
        private int position;
        private List<Status> statuses;

        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public double Crit { get => crit; set => crit = value; }
        public int SpellPower { get => spellPower; set => spellPower = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Level { get => level; set => level = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Position { get => position; set => position = value; }
        public List<Status> Statuses { get => statuses; set => statuses = value; }

        public abstract string setPic();
        public abstract List<int> setStatusTargets(string id, int targetPosition, int enemyCount);
        public abstract int GetTargets(string id);
        public abstract int UseAbility(string id);
        public abstract double setStatusEffect(string id, int targetPosition);

        public virtual void Defend(int incomingDmg)
        {
            var dmg = getDefenseModifier() + incomingDmg;
            dmg = armorEffect(dmg);
            dmg = Convert.ToInt32(dmg * getDefenseMultiplier());
            var shieldedDmg = dmg - getShield();
            reduceShieldAmount(shieldedDmg, dmg);
            if (shieldedDmg > 0)
                Health -= shieldedDmg;
            else Health -= 0;
        }

        public virtual void TrueDmgDefend(int incomingDmg)
        {
            if (incomingDmg > 1)
                Health -= incomingDmg;
            else Health -= 1;
        }

        public virtual void RecieveHeal(int amount)
        {
            if (MaxHealth - Health >= amount)
                Health += amount;
            else Health = MaxHealth;
        }

        public override string ToString()
        {
            var statuses = getStatuses();
            return "Health: " + health +"/" + maxHealth + statuses;
        }

        public void ModifyStatusLength()
        {
            var keepList = new List<Status>();
            foreach (var status in Statuses)
            {
                status.Duration--;
                if (status.Duration > 0)
                {
                    keepList.Add(status);
                }
            }
            Statuses = keepList;
        }

        public void SetStatuses(string id, List<Player> players, List<NPC> enemies, int targetPosition)
        {
            var targetList = setStatusTargets(id, targetPosition, enemies.Count);
            var effect = setStatusEffect(id, targetPosition);
            if (targetList.Count > 0)
            {
                var statuses = Status.Create(id, targetList, effect);
                foreach (var thing in targetList)
                {
                    Character target = players.Find(x => x.Position == thing);
                    if (target == null)
                        target = enemies.Find(x => x.Position == thing);
                    foreach (var status in statuses)
                        if (target.Health >= 0) target.Statuses.Add(status);
                }
            }
        }

        public bool isStunned()
        {
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.Stun) return true;
            }
            return false;
        }

        public virtual void ApplyDoT()
        {
            if (Health > 0)
            {
                foreach (var status in Statuses)
                {
                    if (status is CombatLogicClassLibrary.Statuses.Poison)
                        TrueDmgDefend(Convert.ToInt32(status.Effect));
                    else if (status is CombatLogicClassLibrary.Statuses.DoT)
                        Defend(Convert.ToInt32(status.Effect));
                }
            }
        }

        public void ApplyHoT()
        {
            if (Health > 0)
            {
                foreach (var status in Statuses)
                {
                    if (status is CombatLogicClassLibrary.Statuses.HoT)
                        RecieveHeal(Convert.ToInt32(status.Effect));
                }
            }
        }

        public double GetHealthPercent()
        {
            double value = (double)Health / (double)MaxHealth;
            return value;
        }

        protected double getAttackMultiplier()
        {
            var attackMultiplier = (double)1;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.AttackDmgMultiplier)
                    attackMultiplier += (status.Effect - 1);
            }
            return attackMultiplier;
        }

        protected int getAttackModifier()
        {
            var attackModifier = 0;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.AttackDmgModifier)
                    attackModifier += Convert.ToInt32(status.Effect);
            }
            return attackModifier;
        }

        private int getDefenseModifier()
        {
            var defenseModifier = 0;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.TakenDmgModifier)
                    defenseModifier += Convert.ToInt32(status.Effect);
            }
            return defenseModifier;
        }

        private double getDefenseMultiplier()
        {
            double multiplier = 1;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.TakenDmgMultiplier)
                    multiplier = multiplier * status.Effect;
            }
            return multiplier;
        }

        private int armorEffect(int dmg)
        {
            var effect = dmg - (armor / 4);
            if (effect >= dmg * .25)
                return effect;
            else return Convert.ToInt32(dmg * .25);
        }

        private int getShield()
        {
            var shield = 0;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.Shield)
                    shield += Convert.ToInt32(status.Effect);
            }
            return shield;
        }

        private void reduceShieldAmount(int shieldedDmg, int dmg)
        {
            if (shieldedDmg == dmg)
                return;
            else
            {
                foreach (var status in statuses)
                {
                    if (status is CombatLogicClassLibrary.Statuses.Shield)
                    {
                        if (status.Effect > 0 && status.Effect <= dmg)
                        {
                            dmg -= Convert.ToInt32(status.Effect);
                            status.Effect = 0;
                        }
                        else if (status.Effect > 0 && status.Effect >= dmg)
                        {
                            status.Effect -= dmg;
                            dmg = 0;
                        }
                    }
                }
            }
        }

        protected string getStatuses()
        {
            var value = "";
            foreach (var status in Statuses)
            {
                if (!value.Contains(status.ToString()))
                    value += status.ToString();
            }
            return value;
        }
    }
}
