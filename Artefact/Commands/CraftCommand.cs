using Artefact.CraftingSystem;
using Artefact.Items;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artefact.Commands
{
    class CraftCommand : ICommand
    {
        public string Name => "craft";
        public string[] Aliases => new string[] { "create" };

        public bool HasArguments => false;
        public string NoArgsResponse => "(Item) (Amount)";

        public string Description => "Craft items";

        public void OnRun(List<string> args)
        {
            if (GlobalSettings.SimpleMode)
            {
                List<Item> craftableItems = Item.Items.FindAll((i) => i.IsCraftable);
                List<string> options = craftableItems.Map(i => $"[{ColorConstants.ITEM_COLOR}]{i.Name}[/]").ToList();
                options.Add("Exit");
                int selection = Utils.GetSelection(options.ToArray());
                if (selection >= craftableItems.Count) return;
                Crafting.CraftItem(craftableItems[selection].Name, 1);
            }
            else
            {
                if (args.Count <= 0)
                {
                    Console.WriteLine(NoArgsResponse);
                    return;
                }
                bool lastArgAmount = int.TryParse(args[args.Count - 1], out int amount);
                if (!lastArgAmount) amount = 1;

                string itemName = args.GetRange(0, args.Count - (lastArgAmount ? 1 : 0)).Join(" ");

                Crafting.CraftItem(itemName, amount);
            }
        }
    }
}
