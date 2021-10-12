using Artefact.Commands.Misc;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    class GameState : State
    {
        public override void Init()
        {

        }

        public override void Update()
        {
            if (GlobalSettings.JustLoaded)
            {
                Utils.Type($"Welcome back, [cyan]{GameSettings.PlayerName}");
                GlobalSettings.JustLoaded = false;
            }
            Story.DoStep();
            if (GameSettings.EnableCommands)
            {
                CommandHandler.OnCommand();
            }
        }
    }
}
