using Artefact.Entities;
using Artefact.InventorySystem;
using Artefact.Items.Equipables;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artefact.Commands
{
    class StatusCommand : ICommand
    {
        public string Name => "status";
        public string[] Aliases => new string[] { "player" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Look at your statistics";

        public void OnRun(List<string> args)
        {
            PlayerEntity player = Map.Player;
            Utils.WriteColor($"Player Name: [{ColorConstants.CHARACTER_COLOR}]{GameSettings.PlayerName}[/]");
            Utils.WriteColor($"Health: {Utils.CreateHealthBar(player)}");
            Utils.WriteColor($"Level: [{ColorConstants.XP_COLOR}]{player.Level}");
            Utils.WriteColor($"XP: {Utils.CreateXPBar(player)}");

            if (player.Inventory.Weapon != null)
            {
                Utils.WriteColor($"Weapon Equiped: [{ColorConstants.ITEM_COLOR}]{player.Inventory.Weapon.ItemName}[/]");
            }

            // Sort armor items to have it be from helmet to boots no matter in what order the armor was equipped
            List<KeyValuePair<ArmorType, ItemData>> armorItems = player.Inventory.Armor.ToList();
            armorItems.Sort((a, b) => a.Key.CompareTo(b.Key));

            foreach (KeyValuePair<ArmorType, ItemData> keyValuePair in armorItems)
            {
                Utils.WriteColor($"{keyValuePair.Key} Equipped: [{ColorConstants.ITEM_COLOR}]{keyValuePair.Value.ItemName}[/]");
            }

            Utils.WriteColor($"Attack Damage: [{ColorConstants.GOOD_COLOR}]{player.HitDamage}[/]");
            Utils.WriteColor($"Defense Amount: [{ColorConstants.GOOD_COLOR}]{player.Defense}[/]");
        }
    }
}
