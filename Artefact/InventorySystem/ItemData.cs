using Artefact.Items;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.InventorySystem
{
    [Serializable]
    struct ItemData
    {
        public Item Item { get; }
        public int Amount { get; set; }

        public ItemData(Item item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }

        public string ToColoredString()
        {
            string text = $"[{ColorConstants.ITEM_COLOR}]{Item}[/]: [{ColorConstants.GOOD_COLOR}]{Amount}[/]";

            if (Item is IUsable)
            {
                text += $" - [{ColorConstants.GOOD_COLOR}]Usable[/]";
            }

            return text;
        }

        public override bool Equals(object obj)
        {
            if (obj is ItemData itemData)
            {
                return Item == itemData.Item;
            }
            return false;
        }
    }
}
