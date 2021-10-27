using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.CraftingSystem
{
    static class Crafting
    {
        public static void CraftItem(string itemName)
        {
            Item item = Item.GetItem(itemName);

            if (item == null)
            {
                Utils.WriteColor("[red]That is not an item!");
                return;
            }

            CraftItem(new ItemData(item, 1));
        }

        public static void CraftItem(ItemData item)
        {
            if (!item.Item.IsCraftable)
            {
                Utils.WriteColor($"[magenta]{item.Item}[/] [red]is not craftable!");
                return;
            }

            Inventory inventory = Map.Player.Inventory;

            List<ItemData> missingItems = new List<ItemData>();

            foreach (ItemData craftItem in item.Item.CraftItems)
            {
                if (!inventory.HasItem(craftItem, true))
                {
                    int inventoryAmount = inventory.GetAmount(craftItem.Item);
                    missingItems.Add(new ItemData(craftItem.Item, craftItem.Amount - inventoryAmount));
                }
            }

            if (missingItems.Count > 0)
            {
                Console.WriteLine("You are missing items needed to craft that!");
                Console.WriteLine("Missing:");
                foreach(ItemData itemData in missingItems)
                {
                    Utils.WriteColor($"- {itemData.ToColoredString()}");
                }
                return;
            }

            inventory.AddItem(item, true);

            foreach (ItemData craftItem in item.Item.CraftItems)
            {
                inventory.RemoveItem(craftItem);
            }
        }
    }
}
