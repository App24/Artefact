using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    class HDDRoom : Room
    {
        public override bool StoryRelated => true;
        public override int StoryStep => Story.HDD_STEP;
        public override bool VisitedBool => GameSettings.HDDVisited;

        public HDDRoom() : base(Location.HDD, west: Location.GPU, north: Location.RAM)
        {

        }

        public override void OnInteract()
        {

        }
    }
}
