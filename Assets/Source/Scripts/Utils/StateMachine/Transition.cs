﻿using System;

namespace ArenaHero.Utils.StateMachine
{
    public class Transition<TMachine, TTargetState>
        where TMachine : StateMachine<TMachine> 
        where TTargetState : State<TMachine>
    {
        private readonly StateMachine<TMachine> _machine;
        private readonly Action _reloadScene;

        public Transition(StateMachine<TMachine> stateMachine, Action reloadScene = null)
        {
            _machine = stateMachine;
            _reloadScene = reloadScene;
        }

        public void Transit()
        {
            if (_machine.CurrentState.GetType() != typeof(TTargetState))
            {
                _machine.EnterIn<TTargetState>();
            }

            _reloadScene?.Invoke();
        }
    }
}