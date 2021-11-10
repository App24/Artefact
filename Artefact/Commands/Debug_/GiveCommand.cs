using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class GiveCommand : ICommand
    {
        public string Name => "give";

        public bool HasArguments => true;

        public string NoArgsResponse => "<Item> [Amount]";

        public string[] Aliases => new string[] { };

        public string Description => "Give the player an item";

        public void OnRun(List<string> args)
        {
            bool lastArgAmount = int.TryParse(args[args.Count - 1], out int amount);
            if (!lastArgAmount) amount = 1;

            string itemName = args.GetRange(0, args.Count - (lastArgAmount ? 1 : 0)).Join(" ");

            Item item = Item.GetItem(itemName);

            if (item == null)
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]Not a valid item!");
                return;
            }

            Map.Player.Inventory.AddItem(new ItemData(item, amount));
        }
    }
}
