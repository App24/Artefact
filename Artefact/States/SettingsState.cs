using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    class SettingsState : State
    {
        public override void Init()
        {

        }

        public override void Update()
        {
            Utils.WriteCenter(@"[magenta]
   _____      _   _   _                 
  / ____|    | | | | (_)                
 | (___   ___| |_| |_ _ _ __   __ _ ___ 
  \___ \ / _ | __| __| | '_ \ / _` / __|
  ____) |  __| |_| |_| | | | | (_| \__ \
 |_____/ \___|\__|\__|_|_| |_|\__, |___/
                               __/ |    
                              |___/     
");

            ConsoleColor[] textSpeedColors = new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red };
            ConsoleColor textSpeedColor = textSpeedColors[Array.IndexOf(Enum.GetValues(typeof(TextSpeed)), GlobalSettings.TextSpeed)];
            int selection = Utils.GetSelection($"Text Speed: [{textSpeedColor}]{GlobalSettings.TextSpeed}[/]", $"Simple Mode: [{(GlobalSettings.SimpleMode ? ConsoleColor.Green : ConsoleColor.Red)}]{GlobalSettings.SimpleMode}[/]", "Back");

            switch (selection)
            {
                case 0:
                    {
                        string[] textSpeeds = Enum.GetNames(typeof(TextSpeed));
                        GlobalSettings.TextSpeed = Enum.Parse<TextSpeed>(textSpeeds[Utils.GetSelection(textSpeeds.Map((x, i) => $"[{textSpeedColors[i]}]{x}[/]").ToArray())], true);
                    }break;
                case 1:
                    {
                        GlobalSettings.SimpleMode = !GlobalSettings.SimpleMode;
                    }break;
                case 2:
                    {
                        SaveSystem.SaveSettings();
                        StateMachine.RemoveState();
                    }break;
            }

            Console.Clear();
        }
    }
}
