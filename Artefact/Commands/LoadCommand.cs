using Artefact.Misc;
using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Artefact.Commands
{
    class LoadCommand : ICommand
    {
        public string Name => "load";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            if (Utils.GetConfirmation($"You sure you want to load a save game? [{ColorConstants.BAD_COLOR}]All your progress since last save will be lost!"))
            {
                if (SaveSystem.LoadGame() == LoadResult.Success)
                {
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            }
        }
    }
}
