using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    class PSURoom : Room
    {

        public PSURoom() : base(Location.PSU, south: Location.CPU)
        {

        }

        public override void OnInteract()
        {

        }

        protected override void OnEnterFirst()
        {
            Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]PSU[/]");
            //Dialog.Speak(Character.Clippy, $"Weird, the hard drive isn't spinning, really worrisome. I hope nothing happened to the [{ColorConstants.USER_COLOR}]user[/]");
            Fight.StartFight(Map.Player.Location, new BattleParameters(EnemyType.Electricity, new IntRange(1, 1), 1), new FightParameters(true, true));
        }

        protected override void OnEnterRoom()
        {

        }
    }
}
