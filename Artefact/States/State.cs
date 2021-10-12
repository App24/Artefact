using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    abstract class State
    {
        public abstract void Init();
        public abstract void Update();
    }
}
