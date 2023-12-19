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

        public void InitTransition<TTargetState>(ISubject subject)
            where TTargetState : State<TMachine>
        {
            var transition = new Transition<TMachine, TTargetState>(_stateMachine);

            InitTransition(subject, transition.Transit);
        }

        public void InitTransition(ISubject subject, Action observer) =>
            _subscribtions.Add(new Subscription(subject, observer));

        private void OnEnable()
        {
            if (_subscribtions == null)
            {
                return;
            }

            foreach (var action in _subscribtions)
            {
                action.Subject.ActionEnded += action.Observer;
            }
        }

        private void OnDisable()
        {
            if (_subscribtions == null)
            {
                return;
            }

            foreach (var action in _subscribtions)
            {
                action.Subject.ActionEnded -= action.Observer;
            }
        }
    }
}