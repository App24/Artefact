using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    class GPURoom : Room
    {
        public GPURoom() : base(Location.GPU, Location.CPU, east: Location.HDD)
        {

        }

        public override void OnInteract()
        {

        }
    }
}
