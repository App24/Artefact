using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Items.Equipables;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artefact.Commands
{
    internal class EquipCommand : ICommand
    {
        public string Name => "equip";
        public bool HasArguments => false;

        public string NoArgsResponse => "<Item>";

        public string[] Aliases => new string[] { };

        public string Description => "Equip weapons or armor";

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
                List<Item> availableEquipableItems = inventory.GetItems().Map(i => i.Item).ToList().FindAll(i => i is IEquipable);

                if (availableEquipableItems.Count <= 0)
                {
                    Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You do not have any equipable items!");
                    return;
                }

                List<string> options = availableEquipableItems.Map(i => $"[{ColorConstants.ITEM_COLOR}]{i.Name}[/]").ToList();
                options.Add("Exit");
                int selection = Utils.GetSelection(options.ToArray());

                if (selection >= availableEquipableItems.Count)
                {
                    return;
                }

                item = availableEquipableItems[selection];
            }

            if (!inventory.HasItem(new ItemData(item, 1)))
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You do not have that item in your inventory!");
                return;
            }

            if (!(item is IEquipable))
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]That item is not equipable!");
                return;
            }

            inventory.EquipItem(item);

            Utils.WriteColor($"Equipped: [{ColorConstants.ITEM_COLOR}]{item}");
        }
    }
}
