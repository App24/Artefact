using Artefact.CraftingSystem;
using Artefact.Misc;
using System;

namespace Artefact.Items.Equipables
{
    [Serializable]
    internal class WeaponItem : Item, IEquipable
    {
        public IntRange Damage { get; }

        public EquipableType EquipableType => EquipableType.Weapon;

        public WeaponItem(string name, string description, IntRange damage, params CraftData[] craftData) : base(name, description, craftData)
        {
            Damage = damage;
        }
    }
}
