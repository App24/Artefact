using Artefact.Items;
using Artefact.Misc;
using System;

namespace Artefact.InventorySystem
{
    [Serializable]
    internal class ItemData
    {
        public Item Item => Item.GetItem(ItemName);
        public string ItemName { get; }
        public int Amount { get; set; }

        public ItemData(Item item, int amount = 1) : this(item.Name, amount)
        {
        }

        public ItemData(string itemName, int amount = 1)
        {
            ItemName = itemName;
            Amount = amount;
        }

        public string ToColoredString(bool includeExtra = false)
        {
            string text = $"[{ColorConstants.ITEM_COLOR}]{Item}[/]: [{ColorConstants.GOOD_COLOR}]{Amount}[/]";

            if (Item is IUsable && includeExtra)
            {
                text += $" - [{ColorConstants.GOOD_COLOR}]Usable[/]";
            }

            return text;
        }

        public static ItemData operator *(ItemData a, int b)
        {
            return new ItemData(a.Item, a.Amount * b);
        }

        public static explicit operator ItemData(Item v)
        {
            return new ItemData(v, 1);
        }
    }
}
