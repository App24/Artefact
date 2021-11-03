﻿using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    abstract class Entity
    {
        public string UUID { get; }

        public long MaxHealth { get { return (long)Math.Ceiling(BaseMaxHealth * Math.Pow(1.05f, Math.Max(0, Level - 1))); } protected set { BaseMaxHealth = value; Heal(MaxHealth); } }
        public long BaseMaxHealth { get; private set; }
        public long Health { get; protected set; }
        public Location Location { get; set; }

        // Damage
        public IntRange HitDamage { get { return BaseHitDamage * HitModifierLevel; } protected set { BaseHitDamage = value; } }
        public virtual IntRange BaseHitDamage { get; protected set; }
        public float HitModifierLevel { get { return (float)Math.Pow(1.05f, Math.Max(0, Level - 1)); } }

        // Defense
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

        public long Heal(long amount)
        {
            if (amount < 0)
            {
                return Damage(-amount);
            }

            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
            return amount;
        }

        public long Damage(long amount)
        {
            if (amount < 0)
            {
                return Heal(-amount);
            }
            
            amount = (long)(amount * GetNormalisedDefense());
            Health -= amount;
            if (Health <= 0)
            {
                Kill();
            }
            return amount;
        }

        public float GetNormalisedDefense()
        {
            return 1 - ((Defense / 100f) * DefenseModifier);
        }

        public int GetRandomDamage()
        {
            Random random = new Random();
            return (int)(random.Next(HitDamage.Min, HitDamage.Max + 1) * HitModifierLevel);
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
#if !DISABLE_RNG
            Min = min;
#else
            Min = max;
#endif
            Max = max;
        }

        public IntRange(int amount) : this(amount, amount) { }

        public override string ToString()
        {
            if (Min == Max) return $"{Min}";
            return $"{Min} - {Max}";
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
