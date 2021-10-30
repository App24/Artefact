using Artefact.Entities;
using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Equipables
{
    [Serializable]
    class WeaponItem : EquipableItem
    {
        public IntRange Damage { get; }

        public WeaponItem(string name, IntRange damage, params ItemData[] crafting) : base(name, EquipableType.Weapon, crafting)
        {
            Damage = damage;
        }
    }
}
