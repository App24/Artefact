using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Items.Equipables;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class EquipCommand : ICommand
    {
        public string Name => "equip";
        public bool HasArguments => false;

        public string NoArgsResponse => "<Item>";

        public string[] Aliases => new string[] { };

        public void OnRun(List<string> args)
        {
            Inventory inventory = Map.Player.Inventory;
            Item item = null;
            if (!GlobalSettings.SimpleMode)
            {
                if (args.Count <= 0)
                {
                    Console.WriteLine(NoArgsResponse);
                    return;
                }
                item = Item.GetItem(args.Join(" "));
            }
            else
            {
                List<Item> availableEquipableItems = inventory.GetItems().Map(i => i.Item).FindAll(i => i is EquipableItem);

                int selection = Utils.GetSelection(availableEquipableItems.Map(i => $"[magenta]{i.Name}[/]").ToArray());

                item = availableEquipableItems[selection];
            }

            if (!inventory.HasItem(new ItemData(item, 0)))
            {
                Utils.WriteColor("[red]You do not have that item in your inventory!");
                return;
            }

            if (!(item is EquipableItem))
            {
                Utils.WriteColor("[red]That item is not equipable!");
                return;
            }

            inventory.EquipItem(item);

            Utils.WriteColor($"Equipped: [magenta]{item}");
        }
    }
}
