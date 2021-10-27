using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands.FightCommands
{
    class SweepAttackCommand : ICommand
    {
        public string Name => "sweepattack";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Entities.Move.SweepAttack;
        }
    }
}
