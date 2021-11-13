using Artefact.MapSystem;
using System.Collections.Generic;

namespace Artefact.Commands.FightCommands
{
    internal class SweepAttackCommand : ICommand
    {
        public string Name => "sweepattack";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Attack all the enemies in the fight";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Entities.Move.SweepAttack;
        }
    }
}
