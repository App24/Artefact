using Artefact.Commands.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class HelpCommand : ICommand
    {
        public string Name => "help";

        public string[] Aliases => new string[] { "helpme" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        List<ICommand> commands;

        public HelpCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public void OnRun(List<string> args)
        {
            throw new CommandException("Command yet to be finished!");
        }
    }
}
