using Artefact.MapSystem;
using Artefact.Settings;
using Artefact.StorySystem;
using System;

namespace Artefact.Saving
{
    [Serializable]
    internal class Save
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
