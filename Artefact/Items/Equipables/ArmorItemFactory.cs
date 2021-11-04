using Artefact.CraftingSystem;
using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Equipables
{
    static class ArmorItemFactory
    {
        public static ArmorItems CreateArmor(string name, Item craftItem, int baseDefense)
        {
            ArmorItem helmet=new ArmorItem($"{name} Helmet", baseDefense+1, ArmorType.Helmet, new CraftData(1, new ItemData(craftItem, 5)));
            ArmorItem chestplate=new ArmorItem($"{name} Chestplate", baseDefense+4, ArmorType.Chestplate, new CraftData(1, new ItemData(craftItem, 8)));
            ArmorItem leggings=new ArmorItem($"{name} Leggings", baseDefense+3, ArmorType.Leggings, new CraftData(1, new ItemData(craftItem, 7)));
            ArmorItem boots=new ArmorItem($"{name} Boots", baseDefense, ArmorType.Boots, new CraftData(1, new ItemData(craftItem, 4)));
            return new ArmorItems(helmet, chestplate, leggings, boots);
        }
    }

    struct ArmorItems
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
