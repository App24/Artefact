using Artefact.Misc;
using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class SaveCommand : ICommand
    {
        public string Name => "save";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public void OnRun(List<string> args)
        {
            if (Utils.GetConfirmation("You sure you want to save? [red]It will override the previous saved game!"))
            {
                SaveSystem.SaveGame();
                Console.WriteLine("Game Saved!");
            }
        }
    }
}
