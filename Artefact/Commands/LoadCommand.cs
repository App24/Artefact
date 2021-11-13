using Artefact.Misc;
using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Artefact.Commands
{
    internal class LoadCommand : ICommand
    {
        public string Name => "load";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Load a save game";

        public void OnRun(List<string> args)
        {
            List<string> saves = SaveSystem.GetSaveGameNames();
            saves.Add("Back");
            Utils.WriteColor("[yellow]Please select a save slot!");
            int selection = Utils.GetSelection(saves.ToArray());

            if (selection < saves.Count - 1)
            {
                if (Utils.GetConfirmation($"You sure you want to load a save game? [{ColorConstants.BAD_COLOR}]All your progress since last save will be lost!"))
                {
                    if (SaveSystem.LoadGame(slot: selection + 1) == LoadResult.Success)
                    {
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                }
            }
        }
    }
}
