using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items
{
    [Serializable]
    class BasicItem : Item
    {
        public BasicItem(string name, params ItemData[] crafting) : base(name, crafting)
        {
        }
    }
}
