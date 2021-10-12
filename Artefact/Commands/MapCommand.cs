using Artefact.Commands.Misc;
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

        public void OnRun(List<string> args)
        {
            throw new CommandException("Command not finished");
        }
    }
}
