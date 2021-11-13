using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    class UserRoom : Room
    {
        public UserRoom() : base(Location.Room, east: Location.CPU)
        {
        }

        public override void OnInteract()
        {

        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            GameSettings.ExitedComputer = true;
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
