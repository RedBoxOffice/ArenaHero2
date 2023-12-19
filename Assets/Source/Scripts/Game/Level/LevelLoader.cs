using System;
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
	public class LevelLoader : MonoBehaviour, ISceneLoadHandlerOnState<GameStateMachine>
	{
		[SerializeField] private List<LevelData> _levels;

		private GameStateMachine _gameStateMachine;

		public void OnFightButtonClick()
		{
			Debug.Log($"Fight scene loading - StateMachine = {_gameStateMachine != null}"); // On recall, return FALSE
			SceneLoader.Instance.LoadFight<FightState, LevelData>(_gameStateMachine, _levels[GameDataSaver.Instance.Get<CurrentLevel>().Value]);
		}

		public void OnSceneLoaded<TState>(GameStateMachine machine)
			where TState : State<GameStateMachine>
		{
			_gameStateMachine = machine;
			
			Debug.Log($"Main menu scene loaded - StateMachine = {_gameStateMachine != null}"); // always return TRUE
			GameDataSaver.Instance.Set(new CurrentLevelStage());
		}
	}
}