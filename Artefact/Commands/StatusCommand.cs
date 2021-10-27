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
            Utils.WriteColor($"Player Name: [cyan]{GameSettings.PlayerName}[/]");
            Utils.WriteColor($"Health: {Utils.CreateHealthBar(player)}");

            if (player.Inventory.Weapon != null)
            {
                Utils.WriteColor($"Weapon Equiped: [magenta]{player.Inventory.Weapon}[/]");
            }

            foreach(KeyValuePair<ArmorType, ArmorItem> keyValuePair in player.Inventory.Armor)
            {
                Utils.WriteColor($"{keyValuePair.Key} Equipped: [magenta]{keyValuePair.Value}[/]");
            }

            Utils.WriteColor($"Attack Damage: [darkgreen]{player.HitDamage}[/]");
            Utils.WriteColor($"Defense Amount: [darkgreen]{player.Inventory.Defense}[/]");
        }
    }
}
