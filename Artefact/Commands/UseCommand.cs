using Artefact.Commands.Misc;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class UseCommand : ICommand
    {
        public string Name => "use";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            List<Item> usableItems = Map.Player.Inventory.GetItems().FindAll(i => i.Item is IUsable).Map(i => i.Item);
            if (usableItems.Count <= 0)
            {
                Utils.WriteColor("[red]You do not have any usable items in your inventory!");
                return;
            }

            List<string> options = usableItems.Map(i => i.Name);
            options.Add("Exit");
            int selection = Utils.GetSelection(options.ToArray());
            if (selection >= usableItems.Count)
            {
                throw new CommandException("");
            }

            Item item = usableItems[selection];
            IUsable usableItem = (IUsable)item;

            if (usableItem.OnUse())
            {
                Map.Player.Inventory.RemoveItem(new InventorySystem.ItemData(item, 1));
            }
        }
    }
}
