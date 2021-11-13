using Artefact.MapSystem;
using System.Collections.Generic;

namespace Artefact.Commands.FightCommands
{
    internal class DefendCommand : ICommand
    {
        public string Name => "defend";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Defend against an enemy attack";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Entities.Move.Defend;
        }
    }
}
