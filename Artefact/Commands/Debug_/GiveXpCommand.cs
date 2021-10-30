using Artefact.Commands.Misc;
using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class GiveXpCommand : ICommand
    {
        public string Name => "givexp";

        public string[] Aliases => new string[] { };

        public bool HasArguments => true;

        public string NoArgsResponse => "<xp>";

        public void OnRun(List<string> args)
        {
            if(!int.TryParse(args[0], out int xp))
            {
                throw new CommandException("Invalid Amount");
            }
            Map.Player.AddXP(xp);
        }
    }
}
