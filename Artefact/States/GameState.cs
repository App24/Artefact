﻿using Artefact.Commands.Misc;
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
        CommandHandler instance;

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
                CommandHandler.Instance.OnCommand();
            }
        }

        public override void Pause()
        {
            instance = CommandHandler.Instance;
        }

        public override void Resume()
        {
            CommandHandler.Instance = instance;
        }
    }
}
