using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    class GameOverState : State
    {
        public override void Init()
        {
            Console.Clear();
            Utils.WriteColor("[darkred]YOU DIED!");
        }

        public override void Update()
        {

        }
    }
}
