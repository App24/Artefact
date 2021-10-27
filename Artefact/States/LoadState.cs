using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Artefact.States
{
    class LoadState : State
    {
        public override void Init()
        {

        }

        public override void Update()
        {
            Utils.WriteCenter(@"
[cyan]   _____ _             _      _____                      
[cyan]  / ____| |           | |    / ____|                     
[cyan] | (___ | |_ __ _ _ __| |_  | |  __  __ _ _ __ ___   ___ 
[cyan]  \___ \| __/ _` | '__| __| | | |_ |/ _` | '_ ` _ \ / _ \
[cyan]  ____) | || (_| | |  | |_  | |__| | (_| | | | | | |  __/
[cyan] |_____/ \__\__,_|_|   \__|  \_____|\__,_|_| |_| |_|\___|
                                                         
                                                         
");
            int selection = Utils.GetSelection("Start Game", "Load Game", "Back");

            switch (selection)
            {
                case 0:
                    {
                        SaveSystem.NewGame();
                        StateMachine.RemoveState();
                        StateMachine.AddState(new GameState());
                    }
                    break;
                case 1:
                    {
                        LoadResult loadResult = SaveSystem.LoadGame();
                        Thread.Sleep(1500);
                        if (loadResult == LoadResult.Success)
                        {
                            StateMachine.RemoveState();
                            StateMachine.AddState(new GameState());
                        }
                    }
                    break;
                case 2:
                    {
                        StateMachine.RemoveState();
                    }
                    break;
            }

            Console.Clear();
        }
    }
}
