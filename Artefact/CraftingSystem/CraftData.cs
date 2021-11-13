using Artefact.InventorySystem;
using System;

namespace Artefact.CraftingSystem
{
    [Serializable]
    internal struct CraftData
    {
        public int Amount { get; }
        public ItemData[] CraftItems { get; }

        public CraftData(int amount, params ItemData[] craftItems)
        {
            Amount = amount;
            CraftItems = craftItems;
        }

        public CraftData(params ItemData[] craftItems) : this(1, craftItems)
        {
        }
    }
}
