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

        public virtual int HitDamage { get; protected set; }

        public Entity(int maxHealth, int hitDamage)
        {
            UUID = Guid.NewGuid().ToString();
            MaxHealth = maxHealth;
            Health = MaxHealth;
            HitDamage = hitDamage;
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

        public void Damage(int amount)
        {
            if (amount < 0)
            {
                Heal(-amount);
                return;
            }

            Health -= amount;
            if (Health < 0)
            {
                Map.RemoveEntity(this);
            }
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
    }
}
