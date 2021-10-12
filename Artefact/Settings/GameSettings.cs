using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Settings
{
    [Serializable]
    class GameSettings : Saveable
    {
        public static bool KnowsClippy { get { return Instance.knowsClippy; } set { Instance.knowsClippy = value; } }
        public static string PlayerName { get { return Instance.playerName; } set { Instance.playerName = value; } }
        public static bool EnableCommands { get { return Instance.enableCommands; } set { Instance.enableCommands = value; } }

        public static GameSettings Instance { get; set; }

        bool knowsClippy;
        string playerName;
        bool enableCommands;

        public GameSettings()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
