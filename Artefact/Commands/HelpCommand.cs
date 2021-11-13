using Artefact.Commands.Misc;
using Artefact.DialogSystem;
using Artefact.Misc;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class HelpCommand : ICommand
    {
        public string Name => "help";

        public string[] Aliases => new string[] { "helpme" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Lists the commands available";

        private List<ICommand> commands;

        public HelpCommand(List<ICommand> commands)
        {
            this.commands = new List<ICommand>(commands);
        }

        public void OnRun(List<string> args)
        {
            Dialog.Speak(Character.Clippy, "You need help? Yippy!", instant: true);
            Dialog.Speak(Character.Clippy, "Here is a list of commands you can use to play the game:", instant: true);
            foreach (ICommand command in commands)
            {
                Utils.WriteColor($"[{ColorConstants.COMMAND_COLOR}]{command.Name.ToUpper()}[/] - {command.Description}");
            }
            throw new CommandException("");
        }
    }
}
