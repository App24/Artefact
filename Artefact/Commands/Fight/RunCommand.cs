using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands.FightCommands
{
    class RunCommand : ICommand
    {
        public string Name => "run";

        public string[] Aliases => new string[] { "escape" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Run away from the encounter";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Entities.Move.Run;
        }
    }
}
