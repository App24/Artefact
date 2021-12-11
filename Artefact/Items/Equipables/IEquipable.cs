using Artefact.CraftingSystem;
using System;

namespace Artefact.Items.Equipables
{
    internal interface IEquipable
    {
        EquipableType EquipableType { get; }
    }

    internal enum EquipableType
    {
        Weapon,
        Armor
    }
}
