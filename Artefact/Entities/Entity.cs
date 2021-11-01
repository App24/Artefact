﻿using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    abstract class Entity
    {
        public string UUID { get; }

        public long MaxHealth { get { return (long)Math.Ceiling(_maxHealth * Math.Pow(1.05f, Math.Max(0, Level - 1))); } protected set { _maxHealth = value; Heal(MaxHealth); } }
        private long _maxHealth;
        public long Health { get; protected set; }
        public Location Location { get; set; }

        public virtual IntRange HitDamage { get; protected set; }
        public float HitModifierLevel { get { return (float)Math.Pow(1.05f, Math.Max(0, Level - 1)); } }
        public virtual int Defense { get; protected set; }
        public float DefenseModifier { get; set; } = 1f;
        public Move Move { get; set; }
        public int Level { get; private set; } = 1;
        public int XP { get; private set; }

        public Entity(int maxHealth)
        {
            UUID = Guid.NewGuid().ToString();
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }

        public static bool operator ==(Entity entity1, Entity entity2)
        {
            if (entity1 is null && entity2 is null) return true;
            if (entity1 is null) return false;
            if (entity2 is null) return false;
            return entity1.UUID == entity2.UUID;
        }

        public static bool operator !=(Entity entity1, Entity entity2)
        {
            if (entity1 is null && entity2 is null) return false;
            if (entity1 is null) return true;
            if (entity2 is null) return true;
            return entity1.UUID != entity2.UUID;
        }

        public void Kill()
        {
            Map.RemoveEntity(this);
        }

        public void Heal(long amount)
        {
            if (amount < 0)
            {
                Damage(-amount);
                return;
            }

            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public void Damage(long amount)
        {
            if (amount < 0)
            {
                Heal(-amount);
                return;
            }

            Health -= (int)Math.Ceiling(amount * GetNormalisedDefense());
            if (Health <= 0)
            {
                Kill();
            }
        }

        public float GetNormalisedDefense()
        {
            return 1 - ((Defense / 100f) * DefenseModifier);
        }

        public int GetRandomDamage()
        {
            Random random = new Random();
            return (int)Math.Ceiling(random.Next(HitDamage.Min, HitDamage.Max + 1) * HitModifierLevel);
        }

        public int GetLevelXP()
        {
            return Level * 25 + 25;
        }

        public virtual void AddXP(int amount)
        {
            XP += amount;
            while (XP >= GetLevelXP())
            {
                XP -= GetLevelXP();
                IncreaseLevel();
            }
        }

        public virtual void IncreaseLevel()
        {
            Level++;
            Heal(MaxHealth);
        }

        public void SetLevel(int level)
        {
            Level = level;
            Heal(MaxHealth);
        }
    }

    [Serializable]
    struct IntRange
    {
        public int Min { get; }
        public int Max { get; }

        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public IntRange(int amount) : this(amount, amount) { }

        public override string ToString()
        {
            if (Min == Max) return $"{Min}";
            return $"{Min} - {Max}";
        }

        public static IntRange operator +(IntRange a, IntRange b)
        {
            return new IntRange(a.Min + b.Min, a.Max + b.Max);
        }

        public static IntRange operator -(IntRange a, IntRange b)
        {
            return new IntRange(a.Min - b.Min, a.Max - b.Max);
        }

        public static IntRange operator *(IntRange a, float b)
        {
            return new IntRange((int)Math.Ceiling(a.Min * b), (int)Math.Ceiling(a.Max * b));
        }
    }

    enum Move
    {
        Attack,
        Defend,
        SweepAttack,
        Run,
        InstaKill
    }
}
