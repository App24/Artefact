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

        public void OnRun(List<string> args)
        {
            Item item = Item.GetItem(args[0]);

            if (item == null)
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]Not a valid item!");
                return;
            }

            int amount = 1;

            if (args.Count > 1)
            {
                if (!int.TryParse(args[1], out amount))
                {
                    Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]Please provide a valid amount!");
                    return;
                }
            }

            Map.Player.Inventory.AddItem(new ItemData(item, amount), true);
        }
    }
}
