using Artefact.Items;
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

        public ItemData(Item item, int amount=1)
        {
            Item = item;
            Amount = amount;
        }

        public string ToColoredString()
        {
            return $"[magenta]{Item}[/]: [green]{Amount}[/]";
        }

        public override bool Equals(object obj)
        {
            if(obj is ItemData)
            {
                return Item == ((ItemData)obj).Item;
            }
            return false;
        }
    }
}
