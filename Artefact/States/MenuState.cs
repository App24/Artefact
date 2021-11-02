using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    class MenuState : State
    {
        public override void Init()
        {

        }

        public override void Update()
        {
            Utils.WriteCenter(@"[green]
                _        __           _   
     /\        | |      / _|         | |  
    /  \   _ __| |_ ___| |_ __ _  ___| |_ 
   / /\ \ | '__| __/ _ |  _/ _` |/ __| __|
  / ____ \| |  | ||  __| || (_| | (__| |_ 
 /_/    \_|_|   \__\___|_| \__,_|\___|\__|
                                          
                                          
");
            int selection = Utils.GetSelection("Play Game", "Settings", "Quit");

            switch (selection)
            {
                case 0:
                    {
                        StateMachine.AddState(new StartState(), false);
                    }
                    break;
                case 1:
                    {
                        StateMachine.AddState(new SettingsState(), false);
                    }
                    break;
                case 2:
                    {
                        GlobalSettings.Running = false;
                    }
                    break;
            }

            Console.Clear();
        }
    }
}
