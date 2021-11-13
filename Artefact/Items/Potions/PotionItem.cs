using Artefact.CraftingSystem;
using System;

namespace Artefact.Items.Potions
{
    [Serializable]
    internal abstract class PotionItem : Item, IUsable
    {
        public PotionItem(string name, params CraftData[] craftData) : base(name, craftData) { }

        public abstract bool OnUse();
    }
}
