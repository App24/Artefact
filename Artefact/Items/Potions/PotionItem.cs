using Artefact.CraftingSystem;
using System;

namespace Artefact.Items.Potions
{
    [Serializable]
    internal abstract class PotionItem : Item, IUsable
    {
        public bool IsUsable => true;

        public PotionItem(string name, string description, params CraftData[] craftData) : base(name, description, craftData) { }

        public abstract bool OnUse();
    }
}
