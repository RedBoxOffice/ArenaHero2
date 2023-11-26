using ArenaHero.UI;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
    public class UIFightSceneInitializer : MonoBehaviour
    {
        [SerializeField] private EventTriggerButton _fightWindowButton;
        [SerializeField] private EventTriggerButton _endLevelWindowButton;
        [SerializeField] private EventTriggerButton _pauseWindowButton;

        public void Init(GameStateMachine gameStateMachine)
        {         
            var transitionInitializer = new TransitionInitializer<GameStateMachine>(gameStateMachine);

            transitionInitializer.InitTransition<FightState>(_fightWindowButton);
            transitionInitializer.InitTransition<EndLevelState>(_endLevelWindowButton);
            transitionInitializer.InitTransition<PauseState>(_pauseWindowButton);           
        }
    }
}

