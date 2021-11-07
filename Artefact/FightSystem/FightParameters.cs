using System;

namespace Artefact.FightSystem
{
    class FightParameters
    {
        public Action OnFightEnd { get; set; }
        public bool PreventDeath { get; set; }
        public bool PreventRun { get; set; }

        public FightParameters(bool preventDeath, bool preventRun, Action onFightEnd)
        {
            PreventDeath = preventDeath;
            PreventRun = preventRun;
            OnFightEnd = onFightEnd;
        }

        public FightParameters(Action onFightEnd) : this(false, false, onFightEnd)
        {

        }

        public FightParameters(bool preventDeath, bool preventRun) : this(preventDeath, preventRun, null)
        {
        }

        public FightParameters() : this(false, false)
        {
        }
    }
}
