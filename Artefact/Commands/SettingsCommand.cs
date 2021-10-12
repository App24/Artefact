using Artefact.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class SettingsCommand : ICommand
    {
        public string Name => "settings";
        public string[] Aliases => new string[] { "setting" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            Console.Clear();
            StateMachine.AddState(new SettingsState(), false);
        }
    }
}
