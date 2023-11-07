using System;
using System.Collections.Generic;

namespace ArenaHero.Utils.StateMachine
{
    public class GameStateMachine : StateMachine<GameStateMachine>
    {
        private WindowStateMachine _windowStateMachine;

        public WindowStateMachine Window => _windowStateMachine;

        public GameStateMachine(WindowStateMachine machine, Func<Dictionary<Type, State<GameStateMachine>>> getStates)
            : base(getStates) =>
            _windowStateMachine = machine;

        public void SetWindow<TWindow>()
            where TWindow : WindowState =>
            _windowStateMachine.EnterIn<TWindow>();

        public TState TryGetState<TState>()
            where TState : State<GameStateMachine> =>
            (TState)TryGetState(typeof(TState));
    }
}