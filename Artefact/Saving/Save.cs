using Artefact.MapSystem;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Saving
{
    [Serializable]
    class Save : Saveable
    {
        public GameSettings GameSettings { get; }
        public Map Map { get; }
        public int StoryStep { get; }

        public Save()
        {
            GameSettings = GameSettings.Instance;
            StoryStep = Story.Step;
            Map = Map.Instance;
        }
    }
}
