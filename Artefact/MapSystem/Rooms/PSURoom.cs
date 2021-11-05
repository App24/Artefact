using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    class PSURoom : Room
    {
        public override bool StoryRelated => true;
        public override int StoryStep => Story.PSU_STEP;
        public override bool VisitedBool => GameSettings.PSUVisited;
        public PSURoom() : base(Location.PSU, south: Location.CPU)
        {

        }

        public override void OnInteract()
        {

        }
    }
}
