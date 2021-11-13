using Artefact.Commands.Misc;
using Artefact.MapSystem;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class GiveXpCommand : ICommand
    {
        public string Name => "givexp";

        public string[] Aliases => new string[] { };

        public bool HasArguments => true;

        public string NoArgsResponse => "<xp>";

        public string Description => "Give XP to the player";

        public void OnRun(List<string> args)
        {
            if (!int.TryParse(args[0], out int xp))
            {
                throw new CommandException("Invalid Amount");
            }
            Map.Player.AddXP(xp);
        }
    }
}
