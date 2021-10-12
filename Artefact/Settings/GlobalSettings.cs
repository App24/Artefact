using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Settings
{
    [Serializable]
    class GlobalSettings : Saveable
    {
        public static bool Running { get; set; } = true;
        public static bool JustLoaded { get; set; }

        public static TextSpeed TextSpeed { get { return Instance.textSpeed; } set { Instance.textSpeed = value; } }
        public static bool SimpleMode { get { return Instance.simpleMode; } set { Instance.simpleMode = value; } }

        public static GlobalSettings Instance { get; set; }

        TextSpeed textSpeed = TextSpeed.Normal;
        bool simpleMode;

        public GlobalSettings()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
