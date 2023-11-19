using System;
using System.Collections.Generic;
using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	public class LevelLoader : MonoBehaviour, ISceneLoadHandlerOnState<GameStateMachine, object>
	{
		[SerializeField] private List<LevelData> _levels;

		private Func<CurrentLevel> _getCurrentLevel;
		private GameStateMachine _gameStateMachine;
		
		[Inject]
		private void Inject(ISaver saver) =>
			_getCurrentLevel = () => saver.Get<CurrentLevel>();

		public void OnFightButtonClick() =>
			MenuScene_1.Load<FightState, LevelData>(_gameStateMachine, _levels[_getCurrentLevel().Index]);

		public void OnSceneLoaded<TState>(GameStateMachine machine, object argument = default) where TState : State<GameStateMachine> =>
			_gameStateMachine = machine;
	}
}