using System.Collections.Generic;
using ArenaHero.Data;
using ArenaHero.Debugs;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	public class LevelLoader : MonoBehaviour, ISceneLoadHandlerOnState<GameStateMachine, object>
	{
		[SerializeField] private List<LevelData> _levels;

		private GameStateMachine _gameStateMachine;

		public void OnFightButtonClick() =>
			SceneLoader.Instance.LoadFight<FightState, LevelData>(_gameStateMachine, _levels[GameDataSaver.Instance.Get<CurrentLevel>().Value]);

		public void OnSceneLoaded<TState>(GameStateMachine machine, object argument = default)
			where TState : State<GameStateMachine>
		{
			_gameStateMachine = machine;
			GameDataSaver.Instance.Set(new CurrentLevelStage());
		}
	}
}