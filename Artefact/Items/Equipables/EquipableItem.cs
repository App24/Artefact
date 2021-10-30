using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Equipables
{
    [Serializable]
    abstract class EquipableItem : Item
    {
        public EquipableType EquipableType { get; }

        public EquipableItem(string name, EquipableType equipableType, params ItemData[] crafting) : base(name, crafting)
        {
            EquipableType = equipableType;
        }
    }

    enum EquipableType
    {
        Weapon,
        Armor
    }
}
