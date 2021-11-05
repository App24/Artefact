using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    class CPURoom : Room
    {
        public override bool StoryRelated => true;
        public override int StoryStep => Story.CPU_STEP;
        public override bool VisitedBool => GameSettings.CPUVisited;

        public CPURoom() : base(Location.CPU, south: Location.GPU, east: Location.RAM, north: Location.PSU)
        {

        }

        public override void OnInteract()
        {

        }
    }
}
