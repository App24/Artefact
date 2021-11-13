using System.Collections.Generic;

namespace Artefact.States
{
    internal static class StateMachine
    {
        private static Stack<State> states = new Stack<State>();
        private static State newState;
        private static bool isAdd, isRemove, isReplace;

        public static State ActiveState { get { if (IsEmpty) return null; return states.Peek(); } }
        public static bool IsEmpty { get { return states.Count <= 0; } }

        public static void AddState(State newState, bool replace = true)
        {
            StateMachine.newState = newState;
            isAdd = true;
            isReplace = replace;
        }

        public static void RemoveState()
        {
            isRemove = true;
        }

        public static void ProcessStateChanges()
        {
            if (isRemove && !IsEmpty)
            {
                ActiveState.Remove();

                states.Pop();

                if (!IsEmpty)
                {
                    ActiveState.Resume();
                }

                isRemove = false;
            }

            if (isAdd)
            {
                if (!IsEmpty)
                {
                    if (isReplace)
                    {
                        ActiveState.Remove();
                        states.Pop();
                    }
                    else
                    {
                        ActiveState.Pause();
                    }
                }

                states.Push(newState);
                ActiveState.Init();
                isAdd = false;
            }
        }

        public static void CleanStates()
        {
            for (int i = 0; i < states.Count; i++)
            {
                RemoveState();
                ProcessStateChanges();
            }
        }
    }
}
