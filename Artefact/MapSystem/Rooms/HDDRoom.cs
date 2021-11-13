using Artefact.DialogSystem;
using Artefact.Misc;
using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class HDDRoom : Room
    {

        public HDDRoom() : base(Location.HDD, west: Location.GPU, north: Location.RAM)
        {

        }

        public override void OnInteract()
        {

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
