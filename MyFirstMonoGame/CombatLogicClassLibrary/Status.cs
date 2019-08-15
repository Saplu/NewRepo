using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary
{
    [Serializable]
    public class Status
    {
        private StatusEnums name;
        private int duration;
        private List<int> targets;
        private double effect;

        public StatusEnums Name { get => name; set => name = value; }
        public int Duration { get => duration; set => duration = value; }
        public List<int> Targets { get => targets; set => targets = value; }
        public double Effect { get => effect; set => effect = value; }

        public static List<Status> Create(string id, List<int> targets, double effect)
        {
            var list = new List<Status>();
            switch (id)
            {
                case "Battle Cry": list.Add(new Statuses.AttackDmgMultiplier(2, targets, effect)); return list;
                case "Lava Field": list.Add(new Statuses.DoT(1, targets, Convert.ToInt32(effect))); return list;
                case "Weaken Blood": list.Add(new Statuses.TakenDmgModifier(1, targets, Convert.ToInt32(effect))); return list;
                case "Taunting Blow": list.Add(new Statuses.Taunt(2, targets, Convert.ToInt32(effect)));return list;
                case "Hellfire": list.Add(new Statuses.DoT(3, targets, Convert.ToInt32(effect)));return list;
                case "Sacrifice": list.Add(new Statuses.TakenDmgMultiplier(3, targets, effect));return list;
                case "Challenging Shout": list.Add(new Statuses.Taunt(1, targets, Convert.ToInt32(effect)));return list;
                case "Laser": list.Add(new Statuses.AttackDmgMultiplier(2, targets, effect));return list;
                case "Bubble": list.Add(new Statuses.Shield(3, targets, Convert.ToInt32(effect)));return list;
                case "Healing Words": list.Add(new Statuses.HoT(3, targets, Convert.ToInt32(effect)));return list;
                case "Inspire": list.Add(new Statuses.AttackDmgModifier(2, targets, Convert.ToInt32(effect)));
                    list.Add(new Statuses.AttackDmgMultiplier(2, targets, 1.15));
                    list.Add(new Statuses.TakenDmgModifier(2, targets, Convert.ToInt32(effect))); return list;
                case "Flame Shock": list.Add(new Statuses.DoT(3, targets, Convert.ToInt32(effect))); return list;
                case "Thunder": list.Add(new Statuses.Stun(Convert.ToInt32(effect), targets)); return list;
                case "Stab": list.Add(new Statuses.Poison(3, targets, Convert.ToInt32(effect))); return list;
                case "Mutilate": list.Add(new Statuses.Poison(3, targets, Convert.ToInt32(effect)));
                    list.Add(new Statuses.Poison(3, targets, Convert.ToInt32(effect))); return list;
                case "Jawbreaker": list.Add(new Statuses.Poison(3, targets, Convert.ToInt32(effect)));
                    list.Add(new Statuses.Stun(1, targets)); return list;
                case "Blade Flurry": list.Add(new Statuses.Poison(3, targets, Convert.ToInt32(effect))); return list;
                case "Holy Ground": list.Add(new Statuses.DoT(3, targets, Convert.ToInt32(effect))); return list;
                case "Righteous Bane": list.Add(new Statuses.AttackDmgMultiplier(2, targets, effect)); return list;
                case "BloodPlague": list.Add(new Statuses.DoT(3, targets, Convert.ToInt32(effect))); return list;
                case "RigorMortis": list.Add(new Statuses.AttackDmgMultiplier(2, targets, effect)); return list;
                case "Stunning Blow": list.Add(new Statuses.Stun(Convert.ToInt32(effect), targets)); return list;
                case "Life Leech": list.Add(new Statuses.HoT(1, targets, Convert.ToInt32(effect))); return list;
                case "Holy Shock": list.Add(new Statuses.Taunt(1, targets, Convert.ToInt32(effect))); return list;
                default: return null;
            }
        }
    }
}
