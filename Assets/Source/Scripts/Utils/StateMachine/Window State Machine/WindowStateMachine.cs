using System;
using System.Collections.Generic;

namespace ArenaHero.Utils.StateMachine
{
    public class WindowStateMachine : StateMachine<WindowStateMachine>
    {
        public WindowStateMachine(Func<Dictionary<Type, State<WindowStateMachine>>> getStates) : base(getStates) { }

        public event Action StateUpdated;

        public override void EnterIn<TState>()
        {
            base.EnterIn<TState>();
            StateUpdated?.Invoke();
        }

        public TState TryGetState<TState>(Window window) where TState : State<WindowStateMachine>
        {
            return (TState)TryGetState(window.GetType());
        }
    }
}