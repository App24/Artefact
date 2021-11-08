using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
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

        public string Description => "Save the game";

        public void OnRun(List<string> args)
        {
            if (Utils.GetConfirmation($"You sure you want to save? [{ColorConstants.BAD_COLOR}]It will override the previous saved game!"))
            {
                SaveSystem.SaveGame();
                Utils.WriteColor($"[{ColorConstants.GOOD_COLOR}]Game Saved to Slot: {GameSettings.SaveSlot}!");
            }
        }
    }
}
