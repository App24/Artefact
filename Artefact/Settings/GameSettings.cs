﻿using Artefact.GenderSystem;
using Artefact.Misc;
using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Settings
{
    [Serializable]
    class GameSettings
    {
        #region Player Info
        public static string PlayerName { get { return Instance.playerName; } set { Instance.playerName = value; } }
        public static Gender PlayerGender { get { return Instance.playerGender; } set { Instance.playerGender = value; } }
        public static GenderPronouns Pronouns { get { return Instance.pronouns; } set { Instance.pronouns = value; } }
        #endregion

        #region Misc
        public static bool EnableCommands { get { return Instance.enableCommands; } set { Instance.enableCommands = value; } }
        public static bool KnowsClippy { get { return Instance.knowsClippy; } set { Instance.knowsClippy = value; } }
        public static int SaveSlot { get { return Instance.slot; } set { Instance.slot = value; } }
        #endregion

        #region Visited
        public static bool CPUVisited { get { return Instance.cpuVisited; } set { Instance.cpuVisited = value; } }
        public static bool RAMVisited { get { return Instance.ramVisited; } set { Instance.ramVisited = value; } }
        #endregion

        #region Session Timing
        public static long GameStartDate { get { return Instance.gameStartDate; } set { Instance.gameStartDate = value; } }
        public static long SessionStartDate { get; set; }
        public static long GameTime { get { return Instance.gameTime; } set { Instance.gameTime = value; } }
        #endregion

        public static GameSettings Instance { get; set; }

        bool knowsClippy;
        public string playerName { get; set; }
        Gender playerGender = Gender.Other;
        GenderPronouns pronouns;
        bool enableCommands;
        bool cpuVisited, ramVisited;
        int slot = 1;
        long gameStartDate;
        public long gameTime { get; private set; }

        public GameSettings()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
