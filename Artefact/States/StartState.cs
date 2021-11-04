using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Artefact.States
{
    class StartState : State
    {
        bool saveSlot = false;

        public override void Init()
        {

        }

        public override void Update()
        {
            Utils.WriteCenter(@"[cyan]
   _____ _             _      _____                      
  / ____| |           | |    / ____|                     
 | (___ | |_ __ _ _ __| |_  | |  __  __ _ _ __ ___   ___ 
  \___ \| __/ _` | '__| __| | | |_ |/ _` | '_ ` _ \ / _ \
  ____) | || (_| | |  | |_  | |__| | (_| | | | | | |  __/
 |_____/ \__\__,_|_|   \__|  \_____|\__,_|_| |_| |_|\___|
                                                         
                                                         
");

            if (!saveSlot)
            {
                List<Action> actions = new List<Action>();
                List<string> options = new List<string>();
                options.Add("Start Game");
                actions.Add(() =>
                {
                    saveSlot = true;
                });
                if (SaveSystem.HasAnySaveGames)
                {
                    options.Add("Load Game");
                    actions.Add(() =>
                    {
                        StateMachine.AddState(new LoadState(), false);
                    });
                }
                options.Add("Back");
                actions.Add(() =>
                {
                    StateMachine.RemoveState();
                });
                int selection = Utils.GetSelection(options.ToArray());

                actions[selection]();
            }
            else
            {
                Utils.WriteColor("Select a slot to save your game in!");
                List<string> options = SaveSystem.GetSaveGameNames();
                options.Add("Back");
                int selection = Utils.GetSelection(options.ToArray());

                if (selection == options.Count - 1)
                    saveSlot = false;
                else
                {
                    bool replace = true;
                    if (SaveSystem.HasSavegame(selection + 1))
                    {
                        replace = Utils.GetConfirmation($"[{ColorConstants.WARNING_COLOR}]There is already a save game stored there. [{ColorConstants.BAD_COLOR}]Override it?");
                    }
                    if (replace)
                    {
                        SaveSystem.NewGame();
                        GameSettings.SaveSlot = selection + 1;
                        StateMachine.CleanStates();
                        StateMachine.AddState(new GameState());
                    }
                }
            }

            Console.Clear();
        }
    }
}
