using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System.Collections.Generic;

namespace Artefact.CraftingSystem
{
    internal static class Crafting
    {
        public static void CraftItem(string itemName, int amount)
        {
            Item item = Item.GetItem(itemName);

            if (item == null)
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]That is not an item!");
                return;
            }

            CraftItem(new ItemData(item, amount));
        }

        public static void CraftItem(ItemData item)
        {
            if (!item.Item.IsCraftable)
            {
                Utils.WriteColor($"[{ColorConstants.ITEM_COLOR}]{item.Item}[/] [{ColorConstants.BAD_COLOR}]is not craftable!");
                return;
            }

            Inventory inventory = Map.Player.Inventory;

            List<List<ItemData>> missingItemsRecipes = new List<List<ItemData>>();

            for (int i = 0; i < item.Item.CraftData.Count; i++)
            {
                CraftData craftData = item.Item.CraftData[i];

                List<ItemData> missingItems = new List<ItemData>();

                foreach (ItemData craftItem in craftData.CraftItems)
                {
                    ItemData itemData = craftItem * item.Amount;
                    if (!inventory.HasItem(itemData))
                    {
                        int inventoryAmount = inventory.GetAmount(itemData.Item);
                        missingItems.Add(new ItemData(itemData.Item, itemData.Amount - inventoryAmount));
                    }
                }

                if (missingItems.Count > 0)
                {
                    missingItemsRecipes.Add(missingItems);
                    continue;
                }

                inventory.AddItem(new ItemData(item.Item, craftData.Amount * item.Amount));

                foreach (ItemData craftItem in craftData.CraftItems)
                {
                    inventory.RemoveItem(craftItem * item.Amount);
                }

                missingItemsRecipes.Clear();

                break;
            }

            if (missingItemsRecipes.Count > 0)
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You are missing items needed to craft that!");
                for (int i = 0; i < missingItemsRecipes.Count; i++)
                {
                    List<ItemData> missingItems = missingItemsRecipes[i];
                    Utils.WriteColor($"Recipe: #{i + 1}");
                    Utils.WriteColor("Missing:");
                    foreach (ItemData itemData in missingItems)
                    {
                        Utils.WriteColor($"- {itemData.ToColoredString()}");
                    }
                }
            }
        }
    }
}
