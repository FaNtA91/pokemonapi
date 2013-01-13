/*
 * Credits goes to brunelli1989 for fixing some creatures
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Objects
{
    public class CreatureData
    {
        public string Name;
        public int HitPoints;
        public int Level;
        public int ExperiencePoints;
        public int MaxDamage;
        public FrontAttack FrontAttack;
        public List<DamageType> Types;
        public List<string> Sounds;
        public List<Loot> Loot;

        public CreatureData(string name, int hitPoints, int level, int experiencePoints, int maxDamage, FrontAttack frontAttack, List<DamageType> types, List<string> sounds, List<Loot> loot)
        {
            Name = name;
            HitPoints = hitPoints;
            Level = level;
            ExperiencePoints = experiencePoints;
            MaxDamage = maxDamage;
            Sounds = sounds;
            FrontAttack = frontAttack;
            Types = types;
            Loot = loot;
        }

        public List<DamageType> GetWeakness()
        {
            var damages = Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToList();
            return GetWeakness(damages);
        }

        public List<DamageType> GetWeakness(List<DamageType> types)
        {
            //vou fazer um metodo que pega a fraquesa do poke com base no seus elementos e poderes quando (levitate)

            //if (Weaknesses.Count > 0)
            //{
            //    Weaknesses.Sort();
            //    foreach (var d in Weaknesses.Where(d => types.Contains(d.DamageType)))
            //        return d.DamageType;
            //}

            //if (Strengths.Count > 0)
            //{
            //    Strengths.Sort();
            //    Strengths.Reverse();

            //    foreach (var d in Strengths.Where(d => types.Contains(d.DamageType)))
            //        return d.DamageType;
            //}

            //foreach (var d in types.Where(d => !Immunities.Contains(d)))
            //{
            //    return d;
            //}

            return null;
        }
    }

    public enum DamageType
    {
        Bug,
        Poison,
        Normal,
        Flying,
        Grass,
        Fire,
        Water,
        Ground,
        Electric,
        Rock,
        Fighting,
        Psychic,
        Ice,
        Ghost,
        Dragon,
        Dark,
        Steel
    }

    public class DamageModifier : IComparable<DamageModifier>
    {
        public DamageType DamageType;
        public int Percent;

        public DamageModifier(DamageType type, int percent)
        {
            DamageType = type;
            Percent = percent;
        }

        public int CompareTo(DamageModifier other)
        {
            return Percent.CompareTo(other.Percent);
        }
    }

    public enum LootPossibility
    {
        Always,
        Normal,
        Rare,
        SemiRare,
        VeryRare
    }

    public enum FrontAttack
    {
        None,
        Strike,
        Beam,
        Wave,
        Both
    }

    public struct Loot
    {
        string Name;
        uint Id;
        LootPossibility Possibility;
        int MaxAmmount;

        public Loot(string name, uint id, LootPossibility possibility, int maxAmmout)
        {
            Name = name;
            Id = id;
            Possibility = possibility;
            MaxAmmount = maxAmmout;
        }
    }

}