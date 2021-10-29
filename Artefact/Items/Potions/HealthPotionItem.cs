using Artefact.InventorySystem;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Potions
{
    [Serializable]
    class HealthPotionItem : PotionItem
    {
        public HealthPotionType HealthPotionType { get; }

        public HealthPotionItem(HealthPotionType healthPotionType, params ItemData[] craftings) : base($"{healthPotionType} Health Potion", craftings)
        {
            HealthPotionType = healthPotionType;
        }

        public override bool OnUse()
        {
            if (Map.Player.Health >= Map.Player.MaxHealth)
            {
                Utils.WriteColor("You are at max health already!");
                return false;
            }

            int healthAmount;
            switch (HealthPotionType)
            {
                case HealthPotionType.Small:
                    {
                        healthAmount = 10;
                    }
                    break;
                case HealthPotionType.Medium:
                    {
                        healthAmount = 25;
                    }
                    break;
                case HealthPotionType.Large:
                    {
                        healthAmount = 50;
                    }
                    break;
                default:
                    {
                        Utils.WriteColor($"[darkred]{HealthPotionType} does not have set health regeneration!");
                        return false;
                    }
            }

            Map.Player.Heal(healthAmount);
            return true;
        }
    }

    enum HealthPotionType
    {
        Small,
        Medium,
        Large
    }
}
