using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class GPURoom : Room
    {

        public GPURoom() : base(Location.GPU, Location.CPU, east: Location.HDD)
        {

        }

        public override void OnInteract(ref bool sucess)
        {

        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {

        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
