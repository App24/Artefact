using Artefact.Misc;
using Artefact.Saving;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Artefact.States
{
    internal class LoadState : State
    {
        public override void Init()
        {

        }

        public override void Update()
        {
            Utils.WriteCenter(@"[blue]
  _                     _    _____                      
 | |                   | |  / ____|                     
 | |     ___   __ _  __| | | |  __  __ _ _ __ ___   ___ 
 | |    / _ \ / _` |/ _` | | | |_ |/ _` | '_ ` _ \ / _ \
 | |___| (_) | (_| | (_| | | |__| | (_| | | | | | |  __/
 |______\___/ \__,_|\__,_|  \_____|\__,_|_| |_| |_|\___|
                                                        
                                                        
");
            Utils.WriteColor("Select a slot to load your game!");
            List<string> saves = SaveSystem.GetSaveGameNames();
            saves.Add("Back");
            int selection = Utils.GetSelection(saves.ToArray());

            if (selection == saves.Count - 1)
                StateMachine.RemoveState();
            else
            {
                LoadResult loadResult = SaveSystem.LoadGame(slot: selection + 1);
                Thread.Sleep(1500);
                if (loadResult == LoadResult.Success)
                {
                    StateMachine.CleanStates();
                    StateMachine.AddState(new GameState());
                }
            }

            Console.Clear();
        }
    }
}
