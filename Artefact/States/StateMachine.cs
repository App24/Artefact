using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    static class StateMachine
    {
        static Stack<State> states = new Stack<State>();
        static State newState;
        static bool isAdd, isRemove, isReplace;

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
                //ActiveState.Remove();

                states.Pop();

                if (!IsEmpty)
                {
                    //ActiveState.Resume();
                }

                isRemove = false;
            }

            if (isAdd)
            {
                if (!IsEmpty)
                {
                    if (isReplace)
                    {
                        //ActiveState.Remove();
                        states.Pop();
                    }
                    else
                    {
                        //ActiveState.Pause();
                    }
                }

                states.Push(newState);
                //World.CurrentWorld = ActiveState.World;
                ActiveState.Init();
                isAdd = false;
            }
        }
    }
}
