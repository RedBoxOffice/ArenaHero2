using System.Collections.Generic;
using System;

namespace ArenaHero.Utils.StateMachine
{
    public abstract class StateMachine<TMachine> : IDisposable where TMachine : StateMachine<TMachine>
    {
        private List<State<TMachine>> _states;

        private Dictionary<Type, State<TMachine>> _state;

        public State<TMachine> CurrentState { get; private set; }

        public StateMachine(Func<Dictionary<Type, State<TMachine>>> getStates) => _state = getStates();

        public void Dispose()
        {
            CurrentState?.Exit();
        }

        public virtual void EnterIn<TState>() where TState : State<TMachine>
        {
            if (_state.ContainsKey(typeof(TState)) == false)
                throw new NullReferenceException(nameof(_state));

            if (_state.TryGetValue(typeof(TState), out State<TMachine> state))
            {
                CurrentState?.Exit();
                CurrentState = state;
                CurrentState.Enter();
            }
        }

        protected State<TMachine> TryGetState(Type stateType)
        {
            if (_state.TryGetValue(stateType, out State<TMachine> state))
                return state;
            else 
                return null;
        }
    }
}