using Artefact.Entities;
using Artefact.MapSystem;
using System.Collections.Generic;

namespace Artefact.Commands.FightCommands
{
    internal class AttackCommand : ICommand
    {
        public string Name => "attack";

        public string[] Aliases => new string[] { "fight" };

        public bool HasArguments => false;

        public string NoArgsResponse => "<Enemy>";

        public string Description => "Attack an enemy";

        public void OnRun(List<string> args)
        {
            Map.Player.Move = Move.Attack;
        }
    }
}
