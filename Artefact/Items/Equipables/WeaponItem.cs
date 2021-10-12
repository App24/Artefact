using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Equipables
{
    [Serializable]
    class WeaponItem : EquipableItem
    {
        public int Damage { get; }

        public WeaponItem(string name, int damage, params ItemData[] crafting) : base(name, crafting)
        {
            Damage = damage;
        }
    }
}
