using Artefact.DialogSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Misc;
using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class HDDRoom : Room
    {
        public bool ErasedHardDrive { get; set; }

        public HDDRoom() : base(Location.HDD, west: Location.GPU, north: Location.RAM)
        {

        }

        public override void OnInteract(ref bool success)
        {
            if(!Map.Player.Inventory.HasItem(new ItemData(Item.MagnetItem)))
            {
                success = true;
                Dialog.Speak(Character.Clippy, "There is a magnet over there!");
                if(Utils.GetCharacterConfirmation(Character.Clippy, "Want to pick it up?"))
                {
                    Map.Player.Inventory.AddItem(new ItemData(Item.MagnetItem));
                    Dialog.Speak(Character.Clippy, "You should be careful with that thing, its very powerful");
                    Dialog.Speak(Character.Clippy, "As you can see, im stuck to it");
                }
            }
        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            disableSpawn = true;
            Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]HDD[/]");
            Dialog.Speak(Character.Clippy, $"Weird, the hard drive isn't spinning, really worrisome. I hope nothing happened to the [{ColorConstants.USER_COLOR}]user[/]");
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
