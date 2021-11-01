using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class InventoryCommand : ICommand
    {
        public string Name => "inventory";
        public string[] Aliases => new string[] { "inv" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            Inventory inventory = Map.Player.Inventory;

            Console.WriteLine("Here is what is in your inventory:");
            foreach (ItemData item in inventory.GetItems())
            {
                Utils.WriteColor($"- {item.ToColoredString()}");
            }
        }
    }
}
