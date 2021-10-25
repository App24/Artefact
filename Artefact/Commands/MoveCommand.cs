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

        public string NoArgsResponse => "(Direction)";

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

                if(!Enum.TryParse(args[0], true, out Direction direction))
                {
                    Utils.WriteColor("[red]That is not a valid direction! (north/south/east/west)");
                    return;
                }
                (bool moved, Location location)=Map.Move(direction, Map.Player.Location);
                if (!moved)
                {
                    Utils.WriteColor("[red]You can't move that direction!");
                    return;
                }
                Map.Player.Location = location;
                Utils.WriteColor($"Moved to: [darkcyan]{Map.Player.Location}");
                if (location == Location.CPU && !GameSettings.CPUVisited)
                {
                    Story.Step = Story.CPU_STEP;
                }
                else if (location == Location.RAM && !GameSettings.RAMVisited)
                {
                    Story.Step = Story.RAM_STEP;
                }
            }
        }
    }
}
