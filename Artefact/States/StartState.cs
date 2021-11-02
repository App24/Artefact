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
            List<Action> actions = new List<Action>();
            List<string> options = new List<string>();
            options.Add("Start Game");
            actions.Add(() =>
            {
                SaveSystem.NewGame();
                StateMachine.CleanStates();
                StateMachine.AddState(new GameState());
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

            Console.Clear();
        }
    }
}
