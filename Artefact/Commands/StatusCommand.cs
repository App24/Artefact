using Artefact.Entities;
using Artefact.Items.Equipables;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class StatusCommand : ICommand
    {
        public string Name => "status";
        public string[] Aliases => new string[] { "player" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            PlayerEntity player = Map.Player;
            Utils.WriteColor($"Player Name: [{ColorConstants.CHARACTER_COLOR}]{GameSettings.PlayerName}[/]");
            Utils.WriteColor($"Health: {Utils.CreateHealthBar(player)}");
            Utils.WriteColor($"Level: [{ColorConstants.XP_COLOR}]{player.Level}");
            Utils.WriteColor($"XP: {Utils.CreateXPBar(player)}");

            if (player.Inventory.Weapon != null)
            {
                Utils.WriteColor($"Weapon Equiped: [{ColorConstants.ITEM_COLOR}]{player.Inventory.Weapon}[/]");
            }

            foreach (KeyValuePair<ArmorType, ArmorItem> keyValuePair in player.Inventory.Armor)
            {
                Utils.WriteColor($"{keyValuePair.Key} Equipped: [{ColorConstants.ITEM_COLOR}]{keyValuePair.Value}[/]");
            }

            Utils.WriteColor($"Attack Damage: [{ColorConstants.GOOD_COLOR}]{player.HitDamage * player.HitModifierLevel}[/]");
            Utils.WriteColor($"Defense Amount: [{ColorConstants.GOOD_COLOR}]{player.Defense}[/]");
        }
    }
}
