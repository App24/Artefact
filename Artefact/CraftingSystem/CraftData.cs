using Artefact.InventorySystem;
using System;

namespace Artefact.CraftingSystem
{
    [Serializable]
    internal struct CraftData
    {
        public int CraftAmount { get; }
        public ItemData[] CraftItems { get; }

        public CraftData(int craftAmount, params ItemData[] craftItems)
        {
            CraftAmount = craftAmount;
            CraftItems = craftItems;
        }

        public CraftData(params ItemData[] craftItems) : this(1, craftItems)
        {
        }
    }
}
