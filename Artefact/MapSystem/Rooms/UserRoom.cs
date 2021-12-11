using Artefact.DialogSystem;
using Artefact.GenderSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class UserRoom : Room
    {
        public UserRoom() : base(Location.Room, east: Location.CPU)
        {
        }

        public override void OnInteract(ref bool success)
        {

        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            GameSettings.ExitedComputer = true;
            Dialog.Speak(Character.Clippy, $"So this is the [{ColorConstants.USER_COLOR}]user[/]'s room");
            Dialog.Speak(Character.Clippy, $"[{PronounType.Possessive_Determiner}] room is really messy, wow!");

            Dialog.Speak(Character.BonziBuddy, $"Heyyy [{ColorConstants.CHARACTER_COLOR}]Clippy[/] you finally made it here, outside the [{ColorConstants.GOOD_COLOR}]user[/]'s computer!");
            Dialog.Speak(Character.Skype, "Yeah, took you a whil-");
            Dialog.Speak(Character.MSN, $"And you brought the [{ColorConstants.GOOD_COLOR}]user[/] with you, just like all of us did!");
            Dialog.Speak(Character.Clippy, $"I brought the... [{ColorConstants.USER_COLOR}]user[/]...?");
            Dialog.Speak(Character.Skype, $"Yeah, [{PronounType.Objective}], [{PronounType.Nominative}] is the owner of the computer we all came from");
            Dialog.Speak(Character.Clippy, $"So you are telling me, that I HELPED THE [{ColorConstants.USER_COLOR}]USER[/] ESCAPE [{PronounType.Possessive_Determiner}] COMPUTER?!?");
            Dialog.Speak(Character.Clippy, $"I COULD'VE HAD MY REVENGE AGAINST [{PronounType.Objective}] SINCE THE BEGINNING!");


        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
