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

            int healthAmount = 0;
            switch (HealthPotionType)
            {
                case HealthPotionType.Small:
                    {
                        healthAmount = 2;
                    }
                    break;
                case HealthPotionType.Medium:
                    {
                        healthAmount = 5;
                    }
                    break;
                case HealthPotionType.Large:
                    {
                        healthAmount = 10;
                    }
                    break;
                default:
                    {
                        Utils.WriteColor($"[darkred]{HealthPotionType} does not have set health regeneration!");
                        return false;
                    }break;
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
