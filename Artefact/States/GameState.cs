using Artefact.Commands.Misc;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;

namespace Artefact.States
{
    internal class GameState : State
    {
        private CommandHandler instance;

        public override void Init()
        {

        }

        public override void Update()
        {
            if (GlobalSettings.JustLoaded)
            {
                Utils.Type($"Welcome back, [{ColorConstants.CHARACTER_COLOR}]{GameSettings.PlayerName}");
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
