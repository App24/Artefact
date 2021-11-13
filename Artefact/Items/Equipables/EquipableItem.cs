using Artefact.CraftingSystem;
using System;

namespace Artefact.Items.Equipables
{
    [Serializable]
    internal abstract class EquipableItem : Item
    {
        public EquipableType EquipableType { get; }

        public EquipableItem(string name, EquipableType equipableType, params CraftData[] craftData) : base(name, craftData)
        {
            EquipableType = equipableType;
        }
    }

    internal enum EquipableType
    {
        Weapon,
        Armor
    }
}
