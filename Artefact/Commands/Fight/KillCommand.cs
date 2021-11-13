using Artefact.MapSystem;
using System.Collections.Generic;

namespace Artefact.Commands.FightCommands
{
    internal class KillCommand : ICommand
    {
        public string Name => "instakill";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Insta kill all the enemies";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Entities.Move.InstaKill;
        }
    }
}
