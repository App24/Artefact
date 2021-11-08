using Artefact.DialogSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    class HDDRoom : Room
    {

        public HDDRoom() : base(Location.HDD, west: Location.GPU, north: Location.RAM)
        {

        }

        public override void OnInteract()
        {

        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]HDD[/]");
            Dialog.Speak(Character.Clippy, $"Weird, the hard drive isn't spinning, really worrisome. I hope nothing happened to the [{ColorConstants.USER_COLOR}]user[/]");
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
