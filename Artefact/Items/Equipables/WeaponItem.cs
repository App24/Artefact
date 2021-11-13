using Artefact.CraftingSystem;
using Artefact.Misc;
using System;

namespace Artefact.Items.Equipables
{
    [Serializable]
    internal class WeaponItem : EquipableItem
    {
        public IntRange Damage { get; }

        public WeaponItem(string name, IntRange damage, params CraftData[] craftData) : base(name, EquipableType.Weapon, craftData)
        {
            Damage = damage;
        }
    }
}
