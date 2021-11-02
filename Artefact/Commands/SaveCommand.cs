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
            Utils.WriteColor("[yellow]Please enter a slot number");
            List<string> availableSlots = new List<string>() { "1", "2", "3" };
            availableSlots.Add("Back");
            int selection = Utils.GetSelection(availableSlots.ToArray());

            if (selection < availableSlots.Count - 1)
            {
                if (Utils.GetConfirmation($"You sure you want to save? [{ColorConstants.BAD_COLOR}]It will override the previous saved game!"))
                {
                    SaveSystem.SaveGame(slot: selection + 1);
                    Utils.WriteColor($"[{ColorConstants.GOOD_COLOR}]Game Saved to Slot: {selection + 1}!");
                }
            }
        }
    }
}
