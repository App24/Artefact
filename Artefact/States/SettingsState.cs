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
            Utils.WriteCenter(@"
[yellow]   _____      _   _   _                 
[yellow]  / ____|    | | | | (_)                
[yellow] | (___   ___| |_| |_ _ _ __   __ _ ___ 
[yellow]  \___ \ / _ | __| __| | '_ \ / _` / __|
[yellow]  ____) |  __| |_| |_| | | | | (_| \__ \
[yellow] |_____/ \___|\__|\__|_|_| |_|\__, |___/
[yellow]                               __/ |    
[yellow]                              |___/     
");

            string[] textSpeedColors = new string[] { "green", "yellow", "red" };
            string textSpeedColor = textSpeedColors[Array.IndexOf(Enum.GetValues(typeof(TextSpeed)), GlobalSettings.TextSpeed)];
            string simpleModeColor = GlobalSettings.SimpleMode ? "green" : "red";
            int selection = Utils.GetSelection($"Text Speed: [{textSpeedColor}]{GlobalSettings.TextSpeed}[/]", $"Simple Mode: [{simpleModeColor}]{GlobalSettings.SimpleMode}[/]", "Back");

            switch (selection)
            {
                case 0:
                    {
                        string[] textSpeeds = Enum.GetNames(typeof(TextSpeed));
                        GlobalSettings.TextSpeed = (TextSpeed)Enum.Parse(typeof(TextSpeed), textSpeeds[Utils.GetSelection(textSpeeds.Map((x, i) => $"[{textSpeedColors[i]}]{x}[/]").ToArray())], true);
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
