using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class MoveCommand : ICommand
    {
        public string Name => "go";

        public string[] Aliases => new string[] { "move", "goto" };

        public bool HasArguments => false;

        public string NoArgsResponse => "(Location)";

        public void OnRun(List<string> args)
        {
            if (GlobalSettings.SimpleMode)
            {

            }
            else
            {
                if (args.Count <= 0)
                {
                    Utils.WriteColor(NoArgsResponse);
                    return;
                }

                if(!Enum.TryParse(typeof(Location), args[0], true, out object temp))
                {
                    Utils.WriteColor("[red]That is not a valid location!");
                    return;
                }

                Location location = (Location)temp;

                if (location == Location.Room)
                {
                    Utils.WriteColor("[red]That is not a valid location!");
                    return;
                }

                if (Map.Player.Location == location)
                {
                    Utils.WriteColor($"[yellow]You are already in {location}");
                    return;
                }
                Utils.WriteColor($"[green]Moved to {location}");
                Map.Player.Location = location;
                if (location == Location.CPU && !GameSettings.CPUVisited)
                {
                    Story.Step = Story.CPU_STEP;
                }
                if (location == Location.RAM && !GameSettings.RAMVisited)
                {
                    Story.Step = Story.RAM_STEP;
                }
            }
        }
    }
}
