﻿using Artefact.InventorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Potions
{
    [Serializable]
    abstract class PotionItem : Item, IUsable
    {
        public PotionItem(string name, params ItemData[] craftings) : base(name, craftings) { }

        public abstract bool OnUse();
    }
}
