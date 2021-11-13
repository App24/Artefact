﻿using Artefact.MapSystem;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class DieCommand : ICommand
    {
        public string Name => "die";

        public string[] Aliases => new string[] { "kill" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Kills the player";

        public void OnRun(List<string> args)
        {
            Map.Player.Damage(Map.Player.MaxHealth);
        }
    }
}
