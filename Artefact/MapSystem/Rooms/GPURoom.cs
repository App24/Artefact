using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    class GPURoom : Room
    {

        public GPURoom() : base(Location.GPU, Location.CPU, east: Location.HDD)
        {

        }

        public override void OnInteract()
        {

        }

        protected override void OnEnterFirst()
        {

        }

        protected override void OnEnterRoom()
        {

        }
    }
}
