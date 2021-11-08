using Artefact.Commands.Misc;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class MapCommand : ICommand
    {
        public string Name => "map";
        public string[] Aliases => new string[] { "location" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Find your location";

        public void OnRun(List<string> args)
        {
            Utils.WriteColor($"You are located in [{ColorConstants.LOCATION_COLOR}]{Map.Player.Location}");
            Utils.WriteColor(Map.GetMapLocation(Map.Player.Location));
        }
    }
}
