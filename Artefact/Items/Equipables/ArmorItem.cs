using Artefact.CraftingSystem;
using Artefact.InventorySystem;
using Artefact.Misc;
using System;

namespace Artefact.Items.Equipables
{
    [Serializable]
    internal class ArmorItem : Item, IEquipable
    {
        public int Defense { get; }
        public ArmorType ArmorType { get; }

        public EquipableType EquipableType => EquipableType.Armor;

        public ArmorItem(string name, string description, int defense, ArmorType armorType, params CraftData[] craftData) : base(name, description, craftData)
        {
            Defense = defense;
            ArmorType = armorType;
        }

        public static ArmorItems CreateArmor(string name, Item craftItem, int baseDefense)
        {
            ArmorItem helmet = new ArmorItem($"{name} Helmet", $"A helmet made of [{ColorConstants.ITEM_COLOR}]{craftItem}", baseDefense + 1, ArmorType.Helmet, new CraftData(1, new ItemData(craftItem, 5)));
            ArmorItem chestplate = new ArmorItem($"{name} Chestplate", $"A chestplate made of [{ColorConstants.ITEM_COLOR}]{craftItem}", baseDefense + 4, ArmorType.Chestplate, new CraftData(1, new ItemData(craftItem, 8)));
            ArmorItem leggings = new ArmorItem($"{name} Leggings", $"Leggings made of [{ColorConstants.ITEM_COLOR}]{craftItem}", baseDefense + 3, ArmorType.Leggings, new CraftData(1, new ItemData(craftItem, 7)));
            ArmorItem boots = new ArmorItem($"{name} Boots", $"Boots made of [{ColorConstants.ITEM_COLOR}]{craftItem}", baseDefense, ArmorType.Boots, new CraftData(1, new ItemData(craftItem, 4)));
            return new ArmorItems(helmet, chestplate, leggings, boots);
        }
    }

    internal enum ArmorType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }

    internal struct ArmorItems
    {
        public ArmorItem HelmetItem { get; }
        public ArmorItem ChestplateItem { get; }
        public ArmorItem LeggingsItem { get; }
        public ArmorItem BootsItem { get; }

        public ArmorItems(ArmorItem helmetItem, ArmorItem chestplateItem, ArmorItem leggingsItem, ArmorItem bootsItem)
        {
            HelmetItem = helmetItem;
            ChestplateItem = chestplateItem;
            LeggingsItem = leggingsItem;
            BootsItem = bootsItem;
        }
    }
}
