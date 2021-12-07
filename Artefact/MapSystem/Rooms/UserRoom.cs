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

        public override void OnInteract(ref bool sucess)
        {

        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            GameSettings.ExitedComputer = true;
            Dialog.Speak(Character.Clippy, $"So this is the [{ColorConstants.USER_COLOR}]user[/]'s room");
            Dialog.Speak(Character.Clippy, $"[{PronounType.Possessive_Determiner}] room is really messy, wow!");
            // Thinking
            Dialog.Speak(Character.Player, $"[{ColorConstants.THOUGHT_COLOR}]This looks awfully like my room");
            Dialog.Speak(Character.Clippy, $"[{ColorConstants.BAD_COLOR}]Did you say something?");
            Dialog.Speak(Character.Clippy, $"Are you the [{ColorConstants.USER_COLOR}]user[/]?");
            Utils.ClearPreviousLines();
            Dialog.Speak(Character.Clippy, $"Nah, you can't be [{PronounType.Objective}]");

        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
