using Artefact.CraftingSystem;
using System;

namespace Artefact.Items.Equipables
{
    [Serializable]
    internal class ArmorItem : EquipableItem
    {
        public int Defense { get; }
        public ArmorType ArmorType { get; }

        public ArmorItem(string name, int defense, ArmorType armorType, params CraftData[] craftData) : base(name, EquipableType.Armor, craftData)
        {
            Defense = defense;
            ArmorType = armorType;
        }
    }

    internal enum ArmorType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }
}
