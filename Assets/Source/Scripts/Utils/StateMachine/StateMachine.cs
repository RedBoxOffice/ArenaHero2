using System.Collections.Generic;
using System;

namespace ArenaHero.Utils.StateMachine
{
    public abstract class StateMachine<TMachine> : IDisposable where TMachine : StateMachine<TMachine>
    {
        private List<State<TMachine>> _states;

        private Dictionary<Type, State<TMachine>> _state;

        public State<TMachine> CurrentState { get; private set; }

        protected StateMachine(Func<Dictionary<Type, State<TMachine>>> getStates) => _state = getStates();

        public void Dispose() =>
            CurrentState?.Exit();

        public void EnterIn<TState>() where TState : State<TMachine>
        {
            if (_state.ContainsKey(typeof(TState)) == false)
                throw new NullReferenceException(nameof(_state));

            if (!_state.TryGetValue(typeof(TState), out var state))
                return;
            
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        protected State<TMachine> TryGetState(Type stateType) =>
            _state.TryGetValue(stateType, out var state) ? state : null;
    }
}