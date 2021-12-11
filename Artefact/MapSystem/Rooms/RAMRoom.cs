using Artefact.DialogSystem;
using Artefact.GenderSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Misc;
using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class RAMRoom : Room
    {
        public bool RepairedRAM { get; private set; }

        public RAMRoom() : base(Location.RAM, west: Location.CPU, south: Location.HDD)
        {

        }

        public override void OnInteract(ref bool success)
        {
            if (!RepairedRAM)
            {
                success = true;
                Dialog.Speak(Character.Clippy, "There is some RAM here missing its chips!");
                Dialog.Speak(Character.Clippy, $"It will cost 6 [{ColorConstants.ITEM_COLOR}]{Item.RAMChipItem}[/] to repair it!");
                if (!Map.Player.Inventory.HasItem(new ItemData(Item.RAMChipItem, 6)))
                {
                    Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You do not have enough!");
                    return;
                }
                if (Utils.GetCharacterConfirmation(Character.Clippy, "Do you want to repair it?"))
                {
                    Map.Player.Inventory.RemoveItem(new ItemData(Item.RAMChipItem, 6));
                    Utils.WriteColor($"[{ColorConstants.GOOD_COLOR}]RAM Fixed");
                    RepairedRAM = true;
                }
                else
                {
                    Dialog.Speak(Character.Player, "Maybe later...");
                }
            }
        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            disableSpawn = true;
            Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]RAM[/]");
            Dialog.Speak(Character.Clippy, $"That is odd, there is no data in here, I guess that means that the [{ColorConstants.USER_COLOR}]user[/] does not have [{PronounType.Possessive_Determiner}] computer turned on!");
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
