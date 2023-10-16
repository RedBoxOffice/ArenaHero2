using System;
using System.Collections.Generic;

namespace Base.StateMachine
{
    public class GameStateMachine : StateMachine<GameStateMachine>
    {
        private WindowStateMachine _windowStateMachine;

        public GameStateMachine(Func<Dictionary<Type, State<GameStateMachine>>> getStates) : base(getStates) { }

        public void SetWindow<TWindow>() where TWindow : WindowState
        {
            _windowStateMachine.EnterIn<TWindow>();
        }

        public TState TryGetState<TState>() where TState : State<GameStateMachine>
        {
            return (TState)TryGetState(typeof(TState));
        }
    }
}