using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    abstract class Entity
    {
        public string UUID { get; }

        public int MaxHealth { get; protected set; }
        public int Health { get; protected set; }
        public Location Location { get; set; }

        public virtual IntRange HitDamage { get; protected set; }
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

        public void Damage(int amount)
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

        public void Heal(int amount)
        {
            if (amount < 0)
            {
                Damage(-amount);
                return;
            }

            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public int GetRandomDamage()
        {
            Random random = new Random();
            return random.Next(HitDamage.Min, HitDamage.Max + 1);
        }

        public int GetLevelXP()
        {
            return Level * 50;
        }

        public void AddXP(int amount)
        {
            AddXP(amount, IncreaseLevel);
        }

        protected void AddXP(int amount, Action addXp)
        {
            XP += amount;
            while (XP >= GetLevelXP())
            {
                XP -= GetLevelXP();
                addXp();
            }
        }

        public void IncreaseLevel()
        {
            Level++;
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
    }

    enum Move
    {
        Attack,
        Defend,
        SweepAttack,
        Run
    }
}
