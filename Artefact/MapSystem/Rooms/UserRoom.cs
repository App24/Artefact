﻿using Artefact.Settings;
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
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
