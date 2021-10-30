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

        public static Item MapItem { get; } = new Item("Map");
        public static Item BinaryItem { get; } = new Item("Binary");
        public static Item RecipeBookItem { get; } = new Item("Recipe Book");

        // Weapons
        public static WeaponItem BinarySwordItem { get; } = new WeaponItem("Binary Sword", new IntRange(8, 10), new ItemData(BinaryItem, 3));

        // Armor
        public static ArmorItem BinaryHelmetItem { get; } = new ArmorItem("Binary Helmet", 2, ArmorType.Helmet, new ItemData(BinaryItem, 5));

        // Potions
        public static HealthPotionItem SmallHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Small, new ItemData(BinaryItem, 2));
        public static HealthPotionItem MediumHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Medium, new ItemData(BinaryItem, 5));
        public static HealthPotionItem LargeHealthPotion { get; } = new HealthPotionItem(HealthPotionType.Large, new ItemData(BinaryItem, 8));

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
