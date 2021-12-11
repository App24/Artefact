using Artefact.CraftingSystem;
using Artefact.InventorySystem;
using Artefact.Items.Equipables;
using Artefact.Items.Fighting;
using Artefact.Items.Potions;
using Artefact.Misc;
using System;
using System.Collections.Generic;

namespace Artefact.Items
{
    [Serializable]
    internal class Item
    {
        public string Name { get; }

        public List<CraftData> CraftData { get; }

        #region Normal Items
        public static MagnetItem MagnetItem { get; } = new MagnetItem();
        public static Item MapItem { get; } = new Item("Map", "Find your way around the world");
        public static Item RecipeBookItem { get; } = new Item("Recipe Book", "Learn all the recipes available");
        public static Item ElectronItem { get; } = new Item("Electron", "The power of electricity at your finger tips");
        public static Item ByteItem { get; } = new Item("Byte", "The founding block of data");
        public static Item KiloByteItem { get; } = new Item("KiloByte", "8 bytes", new CraftData(new ItemData(ByteItem, 8)));

        public static Item RAMChipItem { get; } = new Item("RAM Chip", "Important in allowing RAM to work", new CraftData(2, new ItemData(KiloByteItem, 3), new ItemData(ElectronItem, 3)));
        #endregion

        #region Weapons
        public static WeaponItem ByteSwordItem { get; } = new WeaponItem("Byte Sword", "Sword made of bytes", new IntRange(12, 15), new CraftData(new ItemData(ByteItem, 3)));
        public static WeaponItem KiloByteSwordItem { get; } = new WeaponItem("KiloByte Sword", "Sword made of kilobytes", new IntRange(18, 25), new CraftData(new ItemData(KiloByteItem, 3)));

        public static WeaponItem RAMSwordItem { get; } = new WeaponItem("RAM Stick", "Whack Google Chrome for using all the RAM", new IntRange(50, 60), new CraftData(new ItemData(RAMChipItem, 6)));
        #endregion

        #region Armor
        public static ArmorItems ByteArmor { get; } = ArmorItem.CreateArmor("Byte", ByteItem, 8);
        public static ArmorItems KiloByteArmor { get; } = ArmorItem.CreateArmor("KiloByte", KiloByteItem, 12);
        #endregion

        #region Potions
        public static HealthPotionItem SmallHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Small, new CraftData(new ItemData(ByteItem, 2)));
        public static HealthPotionItem MediumHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Medium, new CraftData(new ItemData(ByteItem, 5)));
        public static HealthPotionItem LargeHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Large, new CraftData(new ItemData(KiloByteItem, 2)));
        public static HealthPotionItem GinormousHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Ginormous, new CraftData(new ItemData(KiloByteItem, 6)));
        #endregion

        #region Fighting Items
        public static LightingBolt LightingBoltItem { get; } = new LightingBolt(); // UNLIMITED POWER
        #endregion

        public bool IsCraftable { get { return CraftData.Count > 0; } }

        public static List<Item> Items { get; private set; }

        public static List<Item> CraftableItems { get { return Items.FindAll(i => i.IsCraftable); } }

        public string Description { get; }

        public Item(string name, string description, params CraftData[] craftData)
        {
            Name = name;
            Description = description;
            CraftData = new List<CraftData>(craftData);
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


        public static explicit operator Item(ItemData v)
        {
            return v.Item;
        }
    }
}
