using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Equipables
{
    [Serializable]
    class ArmorItem : EquipableItem
    {
        public int Defense { get; }
        public ArmorType ArmorType { get; }

        public ArmorItem(string name, int defense, ArmorType armorType, params ItemData[] crafting) : base(name, EquipableType.Armor, crafting)
        {
            Defense = defense;
            ArmorType = armorType;
        }
    }

    enum ArmorType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }
}
