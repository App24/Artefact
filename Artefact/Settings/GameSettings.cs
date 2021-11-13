using Artefact.GenderSystem;
using System;

namespace Artefact.Settings
{
    [Serializable]
    internal class GameSettings
    {
        #region Player Info
        public static string PlayerName { get { return Instance.playerName; } set { Instance.playerName = value; } }
        public static Gender PlayerGender { get { return Instance.playerGender; } set { Instance.playerGender = value; } }
        public static GenderPronouns Pronouns { get { return Instance.pronouns; } set { Instance.pronouns = value; } }
        #endregion

        #region Misc
        public static bool EnableCommands { get { return Instance.enableCommands; } set { Instance.enableCommands = value; } }
        public static bool KnowsClippy { get { return Instance.knowsClippy; } set { Instance.knowsClippy = value; } }
        public static bool ExitedComputer { get { return Instance.exitedComputer; } set { Instance.exitedComputer = value; } }
        public static int SaveSlot { get { return Instance.slot; } set { Instance.slot = value; } }
        #endregion

        #region Session Timing
        public static long GameStartDate { get; set; }
        public static long SessionStartDate { get; set; }
        public static long GameTime { get { return Instance.gameTime; } set { Instance.gameTime = value; } }
        #endregion

        public static GameSettings Instance { get; set; }

        private bool knowsClippy;
        public string playerName { get; private set; }

        private Gender playerGender = Gender.Other;
        private GenderPronouns pronouns;
        private bool enableCommands;
        private bool exitedComputer;
        private int slot = 1;
        public long gameTime { get; private set; }

        public GameSettings()
        {
            Instance = this;
        }
    }
}
