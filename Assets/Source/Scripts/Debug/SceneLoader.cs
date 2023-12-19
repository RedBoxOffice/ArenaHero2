using System;
using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Debugs
{
	[Serializable]
	public class SceneLoader
	{
		public static SceneLoader Instance { get; private set; }

		[SerializeField] private bool _isDebugMode;
		[SerializeField] private Debugger _debugger = Debugger.Main;

		public SceneLoader() =>
			Instance ??= this;
		
		public void LoadMenu<TState>(GameStateMachine machine)
			where TState : State<GameStateMachine>
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					// case Debugger.Main:
					// 	MenuScene.Load<TState>(machine);
					// 	break;
					// case Debugger.Slava:
					// 	MenuSceneSlava.Load<TState>(machine);
					// 	break;
					// case Debugger.Dima:
					// 	MenuSceneDima.Load<TState, T>(machine, argument, loadSceneMode);
					// 	break;
					// case Debugger.Soslan:
					// 	MenuSceneSoslan.Load<TState, T>(machine, argument, loadSceneMode);
					// 	break;
				}
			}
			else
			{
				//MenuScene.Load<TState>(machine);
			}
		}
		
		public void LoadFight<TState, T>(GameStateMachine machine, T argument)
			where TState : State<GameStateMachine>
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					// case Debugger.Main:
					// 	FightScene.Load<TState, T>(machine, argument, loadSceneMode);
					// 	break;
					// case Debugger.Slava:
					// 	FightSceneSlava.Load<TState>(machine);
					// 	break;
					// case Debugger.Dima:
					// 	FightSceneDima.Load<TState, T>(machine, argument, loadSceneMode);
					// 	break;
					// case Debugger.Soslan:
					// 	FightSceneSoslan.Load<TState, T>(machine, argument, loadSceneMode);
					// 	break;
				}
			}
			else
			{
				//FightScene.Load<TState, T>(machine, argument, loadSceneMode);
			}
		}

		public void LoadLevelStage(LevelData levelData)
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					// case Debugger.Slava:
					// 	LevelStageHolderScene.Load(levelData, LoadSceneMode.Additive);
				}
			}
		}
	}
}