using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    class RAMRoom : Room
    {
        public override bool StoryRelated => true;
        public override int StoryStep => Story.RAM_STEP;
        public override bool VisitedBool => GameSettings.RAMVisited;
        public RAMRoom() : base(Location.RAM, west: Location.CPU, south: Location.HDD)
        {

        }

        public override void OnInteract()
        {

        }
    }
}
