using ArenaHero.UI;
using ArenaHero.Utils.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
    public class UIFightSceneInitializer : MonoBehaviour
    {
        [SerializeField] private EventTriggerButton _fightWindowButton;
        [SerializeField] private EventTriggerButton _menuWindowButton;
        [SerializeField] private EventTriggerButton _pauseWindowButton;

        public void Init(GameStateMachine _gameStateMachine)
        {
            Debug.Log("Init");
            var transitionInitializer = new TransitionInitializer<GameStateMachine>(_gameStateMachine);

            transitionInitializer.InitTransition<FightState>(_fightWindowButton);
            transitionInitializer.InitTransition<MenuState>(_menuWindowButton);
            transitionInitializer.InitTransition<PauseState>(_pauseWindowButton);           
        }
    }
}

