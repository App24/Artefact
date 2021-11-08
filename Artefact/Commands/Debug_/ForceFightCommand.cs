using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.MapSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class ForceFightCommand : ICommand
    {
        public string Name => "ffight";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Forces an enemy encounter";

        public void OnRun(List<string> args)
        {
            Fight.StartFight(Map.Player.Location, new BattleParameters(EnemyType.Virus | EnemyType.Trojan | EnemyType.RansomWare | EnemyType.AdWare | EnemyType.SpyWare));
        }
    }
}
