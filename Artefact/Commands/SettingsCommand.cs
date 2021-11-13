using Artefact.States;
using System;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class SettingsCommand : ICommand
    {
        public string Name => "settings";
        public string[] Aliases => new string[] { "setting" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Change settings";

        public void OnRun(List<string> args)
        {
            Console.Clear();
            StateMachine.AddState(new SettingsState(), false);
        }
    }
}
