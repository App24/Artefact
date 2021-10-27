﻿using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
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
            Direction direction;
            if (!GlobalSettings.SimpleMode)
            {
                if (args.Count <= 0)
                {
                    Utils.WriteColor(NoArgsResponse);
                    return;
                }

                if (!Enum.TryParse(args[0], true, out direction))
                {
                    Utils.WriteColor("[red]That is not a valid direction! (north/south/east/west)");
                    return;
                }
            }
            else
            {
                string[] directions = Enum.GetNames(typeof(Direction));

                Utils.WriteColor("Please select a direction");
                direction = (Direction)Utils.GetSelection(directions);
            }

            if(!Map.MovePlayer(direction))
                Utils.WriteColor("[red]You can't move that direction!");
        }
    }
}
