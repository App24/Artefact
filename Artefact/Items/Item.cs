using Artefact.InventorySystem;
using Artefact.Items.Equipables;
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

        public static BasicItem MapItem { get; } = new BasicItem("Map");
        public static BasicItem BinaryItem { get; } = new BasicItem("Binary");
        public static BasicItem RecipeBookItem { get; } = new BasicItem("Recipe Book");

        //Weapons
        public static WeaponItem BinarySwordItem { get; } = new WeaponItem("Binary Sword", 2, new ItemData(BinaryItem, 3));

        //Armor
        public static ArmorItem BinaryHelmetItem { get; } = new ArmorItem("Binary Helmet", 2, ArmorType.Helmet, new ItemData(BinaryItem, 5));

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
