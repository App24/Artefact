using Artefact.CraftingSystem;
using Artefact.Items;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class CraftCommand : ICommand
    {
        public string Name => "craft";
        public string[] Aliases => new string[] { "create" };

        public bool HasArguments => false;
        public string NoArgsResponse => "(Item)";

        public void OnRun(List<string> args)
        {
            if (GlobalSettings.SimpleMode)
            {
                List<Item> craftableItems = Item.Items.FindAll((i) => i.IsCraftable);
                List<string> options = craftableItems.Map(i => $"[{ColorConstants.ITEM_COLOR}]{i.Name}[/]");
                options.Add("Exit");
                int selection = Utils.GetSelection(options.ToArray());
                if (selection >= craftableItems.Count) return;
                Crafting.CraftItem(craftableItems[selection].Name);
            }
            else
            {
                if (args.Count <= 0)
                {
                    Console.WriteLine(NoArgsResponse);
                    return;
                }
                string itemName = args.Join(" ");

                Crafting.CraftItem(itemName);
            }
        }
    }
}
