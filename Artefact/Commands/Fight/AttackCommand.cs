using Artefact.Commands.Misc;
using Artefact.Entities;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands.FightCommands
{
    class AttackCommand : ICommand
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
