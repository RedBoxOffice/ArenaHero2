using System;
using ArenaHero.UI;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
    public class UIFightSceneInitializer : MonoBehaviour
    {
        [SerializeField] private EventTriggerButton _fightWindowButton;
        [SerializeField] private EventTriggerButton _endLevelWindowButton;
        [SerializeField] private EventTriggerButton _pauseWindowButton;

        private Action _onEnableTransitions;
        private Action _onDisableTransitions;

        private void OnEnable() =>
            _onEnableTransitions?.Invoke();

        private void OnDisable() =>
            _onDisableTransitions?.Invoke();
        
        public void Init(GameStateMachine gameStateMachine)
        {         
            var transitionInitializer = new TransitionInitializer<GameStateMachine>(gameStateMachine, out _onEnableTransitions, out _onDisableTransitions);

            transitionInitializer.InitTransition<FightState>(_fightWindowButton);
            transitionInitializer.InitTransition<EndLevelState>(_endLevelWindowButton);
            transitionInitializer.InitTransition<PauseState>(_pauseWindowButton);           
        }
    }
}

