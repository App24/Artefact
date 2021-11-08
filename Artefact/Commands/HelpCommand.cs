using Artefact.Commands.Misc;
using Artefact.DialogSystem;
using Artefact.Misc;
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

        public string Description => "Lists the commands available";

        List<ICommand> commands;

        public HelpCommand(List<ICommand> commands)
        {
            this.commands = new List<ICommand>(commands);
        }

        public void OnRun(List<string> args)
        {
            Dialog.Speak(Character.Clippy, "Here is a list of commands you can use:", instant: true);
            foreach (ICommand command in commands)
            {
                Utils.WriteColor($"[{ColorConstants.COMMAND_COLOR}]{command.Name}[/] - {command.Description}");
            }
            throw new CommandException("");
        }
    }
}
