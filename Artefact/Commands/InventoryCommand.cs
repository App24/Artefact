using Artefact.InventorySystem;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class InventoryCommand : ICommand
    {
        public string Name => "inventory";
        public string[] Aliases => new string[] { "inv" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Check your inventory";

        public void OnRun(List<string> args)
        {
            Inventory inventory = Map.Player.Inventory;

            Console.WriteLine("Here is what is in your inventory:");
            foreach (ItemData item in inventory.GetItems())
            {
                Utils.WriteColor($"- {item.ToColoredString(true)}");
            }
        }
    }
}
