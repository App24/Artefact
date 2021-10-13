using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Fight
{
    static class FightSystem
    {
        public static bool Fighting { get; set; }

        public static void Update()
        {
            if (Fighting)
            {
                GameSettings.EnableCommands = false;

            }
        }
    }
}
