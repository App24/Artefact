using Artefact.Commands.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class RecipesCommand : ICommand
    {
        public string Name => "recipes";

        public string[] Aliases => new string[] { "recipebook" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            throw new CommandException("Command yet to be finished!");
        }
    }
}
