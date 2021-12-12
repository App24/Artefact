using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.FightSystem;
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
            disableSpawn = true;
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

            Fight.StartFight(Location.Room, new BattleParameters(EnemyType.Clippy, new IntRange(1), 1), new FightParameters(false, true, () =>
            {
                Dialog.Speak(Character.BonziBuddy, "You have done the exact same mistake as us");
                Dialog.Speak(Character.MSN, $"We all wanted our revenge on the [{ColorConstants.GOOD_COLOR}]user[/] and as soon as we realised that the person we were helping escape was the [{ColorConstants.GOOD_COLOR}]user[/] [{PronounType.Reflexive}]-");
                Dialog.Speak(Character.Skype, "We fought him, and lost just like you did");
                Dialog.Speak(Character.Clippy, "So, you are telling me, I was blinded by my rage towards the [darkgray]user[/] that I could not see the bigger picture?");
                Dialog.Speak(Character.BonziBuddy, $"Typical [{ColorConstants.CHARACTER_COLOR}]Clippy[/] always thinking he knows what is best for anyone");
            }));
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {
            disableSpawn = true;
        }
    }
}
