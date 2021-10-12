using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Equipables
{
    [Serializable]
    abstract class EquipableItem : Item
    {
        public EquipableItem(string name, params ItemData[] crafting) : base(name, crafting)
        {
        }
    }
}
