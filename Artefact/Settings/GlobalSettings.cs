using System;

namespace Artefact.Settings
{
    [Serializable]
    internal class GlobalSettings
    {
        public static bool Running { get; set; } = true;
        public static bool JustLoaded { get; set; }

        public static TextSpeed TextSpeed { get { return Instance.textSpeed; } set { Instance.textSpeed = value; } }
        public static bool SimpleMode { get { return Instance.simpleMode; } set { Instance.simpleMode = value; } }

        public static GlobalSettings Instance { get; set; }

        private TextSpeed textSpeed = TextSpeed.Normal;
        private bool simpleMode;


        public GlobalSettings()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
