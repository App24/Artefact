using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands.FightCommands
{
    class KillCommand : ICommand
    {
        public string Name => "instakill";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Entities.Move.InstaKill;
        }
    }
}
