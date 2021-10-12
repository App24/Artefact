using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    class PlayerEntity : Entity
    {
        public Inventory Inventory { get; }

        public override int HitDamage => Inventory.Weapon == null ? 1 : Inventory.Weapon.Damage;

        public PlayerEntity() : base(20, 1)
        {
            Inventory = new Inventory();
        }
    }
}
