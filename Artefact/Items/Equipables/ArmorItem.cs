using Artefact.CraftingSystem;
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

        public ArmorItem(string name, int defense, ArmorType armorType, params CraftData[] craftData) : base(name, EquipableType.Armor, craftData)
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
