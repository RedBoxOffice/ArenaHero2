using System;
using System.Collections.Generic;

namespace ArenaHero.Utils.StateMachine
{
    public class TransitionInitializer<TMachine> where TMachine : StateMachine<TMachine>
    {
        private readonly TMachine _stateMachine;
        private readonly List<Subscription> _subscribtions = new List<Subscription>();

        public TransitionInitializer(TMachine stateMachine, out Action onEnable, out Action onDisable)
        {
            onEnable = OnEnable;
            onDisable = OnDisable;
            
            _stateMachine = stateMachine;
        }

        public void InitTransition<TTargetState>(ISubject subject, Action reloadScene = null)
            where TTargetState : State<TMachine>
        {
            var transition = new Transition<TMachine, TTargetState>(_stateMachine, reloadScene);

            subject.ActionEnded += transition.Transit;

            _subscribtions.Add(new Subscription(subject, transition.Transit));
        }
        
        private void OnEnable()
        {
            if (_subscribtions != null)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_subscribtions != null)
                UnSubscribe();
        }

        private void Subscribe()
        {
            foreach (var action in _subscribtions)
                action.Subject.ActionEnded += action.Observer;
        }

        private void UnSubscribe()
        {
            foreach (var action in _subscribtions)
                action.Subject.ActionEnded -= action.Observer;
        }
    }
}