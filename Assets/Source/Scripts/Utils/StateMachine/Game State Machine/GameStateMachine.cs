using System;
using System.Collections.Generic;

namespace ArenaHero.Utils.StateMachine
{
    public class GameStateMachine : StateMachine<GameStateMachine>
    {
        public GameStateMachine(WindowStateMachine machine, Func<Dictionary<Type, State<GameStateMachine>>> getStates)
            : base(getStates) =>
            Window = machine;

        public WindowStateMachine Window { get; }

        public TState TryGetState<TState>()
            where TState : State<GameStateMachine> =>
            (TState)TryGetState(typeof(TState));
    }
}