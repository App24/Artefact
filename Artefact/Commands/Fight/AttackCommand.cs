using Artefact.Commands.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands.FightCommands
{
    class AttackCommand : ICommand
    {
        public string Name => "attack";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "<Enemy>";

        public void OnRun(List<string> args)
        {
            throw new CommandException("Not finished");
        }
    }
}
