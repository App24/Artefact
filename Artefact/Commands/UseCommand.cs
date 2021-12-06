using Artefact.Commands.Misc;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System.Collections.Generic;
using System.Linq;

namespace Artefact.Commands
{
    internal class UseCommand : ICommand
    {
        public string Name => "use";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Use items in your inventory";

        public void OnRun(List<string> args)
        {
            List<Item> usableItems = Map.Player.Inventory.GetItems().FindAll(i => i.Item is IUsable usable && usable.IsUsable).Map(i => i.Item).ToList();
            if (usableItems.Count <= 0)
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You do not have any usable items in your inventory!");
                throw new CommandException("");
            }

            List<string> options = usableItems.Map(i => $"[{ColorConstants.ITEM_COLOR}]{i.Name}[/]").ToList();
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
                Map.Player.Inventory.RemoveItem(new ItemData(item));
            }
        }
    }
}
