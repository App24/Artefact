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
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You are at max health already!");
                return false;
            }

            float healthAmount;
            switch (HealthPotionType)
            {
                case HealthPotionType.Small:
                    {
                        healthAmount = Map.Player.MaxHealth / 10f;
                    }
                    break;
                case HealthPotionType.Medium:
                    {
                        healthAmount = Map.Player.MaxHealth / 4f;
                    }
                    break;
                case HealthPotionType.Large:
                    {
                        healthAmount = Map.Player.MaxHealth / 2f;
                    }
                    break;
                default:
                    {
                        Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]{HealthPotionType} does not have set health regeneration!");
                        return false;
                    }
            }

            Map.Player.Heal((int)healthAmount);
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
