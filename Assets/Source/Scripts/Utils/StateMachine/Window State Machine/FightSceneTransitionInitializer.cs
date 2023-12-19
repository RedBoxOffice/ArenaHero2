using System;
using ArenaHero.Data;
using ArenaHero.Debugs;
using ArenaHero.UI;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
	public class FightSceneTransitionInitializer : MonoBehaviour
	{
		[SerializeField] private PauseButton _pauseButton;
		[SerializeField] private ResumeButton _resumeButton;
		[SerializeField] private MainMenuButton _mainMenuButton;
		[SerializeField] private NextStageButton _nextStageButton;

		private Action _onEnableTransitions;
		private Action _onDisableTransitions;

		private void OnEnable() =>
			_onEnableTransitions?.Invoke();

		private void OnDisable() =>
			_onDisableTransitions?.Invoke();

		public void Init(GameStateMachine gameStateMachine, LevelData levelData, ISubject heroDied, ISubject endLevelHandler)
		{
			var transitionInitializer = new TransitionInitializer<GameStateMachine>(gameStateMachine, out _onEnableTransitions, out _onDisableTransitions);

			transitionInitializer.InitTransition<FightState>(_resumeButton);
			transitionInitializer.InitTransition<PauseState>(_pauseButton);
			transitionInitializer.InitTransition<EndLevelState>(heroDied);
			transitionInitializer.InitTransition<EndLevelState>(endLevelHandler);
			transitionInitializer.InitTransition(_mainMenuButton, () => SceneLoader.Instance.LoadMenu<MenuState, object>(gameStateMachine));
			transitionInitializer.InitTransition(_nextStageButton, () => SceneLoader.Instance.LoadFight<FightState, LevelData>(gameStateMachine, levelData));
		}
	}
}