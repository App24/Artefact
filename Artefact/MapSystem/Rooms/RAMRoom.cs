using Artefact.DialogSystem;
using Artefact.GenderSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    class RAMRoom : Room
    {

        public RAMRoom() : base(Location.RAM, west: Location.CPU, south: Location.HDD)
        {

        }

        public override void OnInteract()
        {

        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]RAM[/]");
            Dialog.Speak(Character.Clippy, $"That is odd, there is no data in here, I guess that means that the [{ColorConstants.USER_COLOR}]user[/] does not have [{PronounType.Possessive_Determiner}] computer turned on!");
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
