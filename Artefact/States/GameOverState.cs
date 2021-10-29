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
            Utils.WriteColor("[darkred]YOU DIED!");

            Utils.WriteColor("Would you like to restart from checkpoint?");
            int selection = Utils.GetSelection("Yes", "No");
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
