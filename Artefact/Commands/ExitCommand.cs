using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class ExitCommand : ICommand
    {
        public string Name => "exit";
        public string[] Aliases => new string[] { "quit" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Exit the game";

        public void OnRun(List<string> args)
        {
            if (Utils.GetConfirmation($"You sure you want to [{ColorConstants.BAD_COLOR}]exit[/]?"))
            {
                if (Utils.GetConfirmation($"Do you want to [{ColorConstants.GOOD_COLOR}]save[/]?"))
                {
                    SaveSystem.SaveGame();
                }
                if (!Utils.GetConfirmation($"Return to [{ColorConstants.WARNING_COLOR}]main menu?"))
                {
                    GlobalSettings.Running = false;
                }
                else
                {
                    Console.Clear();
                    StateMachine.RemoveState();
                    StateMachine.AddState(new MenuState());
                }
            }
        }
    }
}
