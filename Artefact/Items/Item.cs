using Artefact.Entities;
using Artefact.InventorySystem;
using Artefact.Items.Equipables;
using Artefact.Items.Potions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items
{
    [Serializable]
    class Item
    {
        public string Name { get; }

        public List<ItemData> CraftItems { get; }

        #region Normal Items
        public static Item MapItem { get; } = new Item("Map");
        public static Item RecipeBookItem { get; } = new Item("Recipe Book");
        public static Item BitItem { get; } = new Item("Bit");
        public static Item ByteItem { get; } = new Item("Byte", new ItemData(BitItem, 8));
        public static Item KiloByteItem { get; } = new Item("KiloByte", new ItemData(ByteItem, 8));
        #endregion

        #region Weapons
        public static WeaponItem BitSwordItem { get; } = new WeaponItem("Bit Sword", new IntRange(8, 10), new ItemData(BitItem, 3));
        public static WeaponItem ByteSwordItem { get; } = new WeaponItem("Byte Sword", new IntRange(12, 15), new ItemData(ByteItem, 3));
        public static WeaponItem KiloByteSwordItem { get; } = new WeaponItem("KiloByte Sword", new IntRange(18, 25), new ItemData(KiloByteItem, 3));

        public static WeaponItem RAMSwordItem { get; } = new WeaponItem("RAM Stick", new IntRange(50, 60), new ItemData(KiloByteItem, 20));
        #endregion

        #region Armor
        #region Bit
        public static ArmorItem BitHelmetItem { get; } = new ArmorItem("Bit Helmet", 2, ArmorType.Helmet, new ItemData(BitItem, 5));
        public static ArmorItem BitChestplateItem { get; } = new ArmorItem("Bit Chestplate", 4, ArmorType.Chestplate, new ItemData(BitItem, 8));
        public static ArmorItem BitLeggingsItem { get; } = new ArmorItem("Bit Leggings", 3, ArmorType.Leggings, new ItemData(BitItem, 7));
        public static ArmorItem BitBootsItem { get; } = new ArmorItem("Bit Boots", 2, ArmorType.Boots, new ItemData(BitItem, 4));
        #endregion

        #region Byte
        public static ArmorItem ByteHelmetItem { get; } = new ArmorItem("Byte Helmet", 5, ArmorType.Helmet, new ItemData(ByteItem, 5));
        public static ArmorItem ByteChestplateItem { get; } = new ArmorItem("Byte Chestplate", 9, ArmorType.Chestplate, new ItemData(ByteItem, 8));
        public static ArmorItem ByteLeggingsItem { get; } = new ArmorItem("Byte Leggings", 7, ArmorType.Leggings, new ItemData(ByteItem, 7));
        public static ArmorItem ByteBootsItem { get; } = new ArmorItem("Byte Boots", 4, ArmorType.Boots, new ItemData(ByteItem, 4));
        #endregion
        #endregion

        #region Potions
        public static HealthPotionItem SmallHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Small, new ItemData(BitItem, 2));
        public static HealthPotionItem MediumHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Medium, new ItemData(BitItem, 5));
        public static HealthPotionItem LargeHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Large, new ItemData(ByteItem, 2));
        public static HealthPotionItem GinormousHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Ginormous, new ItemData(ByteItem, 6));
        #endregion

        public bool IsCraftable { get { return CraftItems.Count > 0; } }

        public static List<Item> Items { get; private set; }

        public Item(string name, params ItemData[] crafting)
        {
            Name = name;
            CraftItems = new List<ItemData>(crafting);
            if (Items == null) Items = new List<Item>();
            Items.Add(this);
        }

        public static bool operator ==(Item item1, Item item2)
        {
            if (item1 is null && item2 is null) return true;
            if (item1 is null) return false;
            if (item2 is null) return false;
            return item1.Name == item2.Name;
        }

        public static bool operator !=(Item item1, Item item2)
        {
            if (item1 is null && item2 is null) return false;
            if (item1 is null) return true;
            if (item2 is null) return true;
            return item1.Name != item2.Name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static Item GetItem(string name)
        {
            return Items.Find(i => i.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
