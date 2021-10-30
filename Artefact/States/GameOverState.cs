using Artefact.Misc;
using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Artefact.States
{
    class GameOverState : State
    {
        public override void Init()
        {
            Console.Clear();
        }

        public override void Update()
        {
            Utils.WriteCenter(@"[darkred]
 __     __           _____  _          _ _ 
 \ \   / /          |  __ \(_)        | | |
  \ \_/ ___  _   _  | |  | |_  ___  __| | |
   \   / _ \| | | | | |  | | |/ _ \/ _` | |
    | | (_) | |_| | | |__| | |  __| (_| |_|
    |_|\___/ \__,_| |_____/|_|\___|\__,_(_)
                                           
                                           ");

            Utils.WriteColor("Would you like to restart from checkpoint?");
            int selection = Utils.GetSelection($"[{ColorConstants.GOOD_COLOR}]Yes", $"[{ColorConstants.BAD_COLOR}]No");
            if (selection == 0)
            {
                LoadResult loadResult = SaveSystem.LoadGame(SaveSystem.CHECKPOINT_FILE);
                Thread.Sleep(1500);
                if (loadResult == LoadResult.Success)
                {
                    StateMachine.CleanStates();
                    StateMachine.AddState(new GameState());
                }
            }
            else
            {
                StateMachine.CleanStates();
                StateMachine.AddState(new MenuState());
            }

            Console.Clear();
        }
    }
}
