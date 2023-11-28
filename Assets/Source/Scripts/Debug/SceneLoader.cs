using System;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using ArenaHero.Utils.UnityTool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Debugs
{
	[Serializable]
	public class SceneLoader
	{
		[SerializeField] private bool _isDebugMode;
		[SerializeField] private Debugger _debugger = Debugger.Main;

		public void LoadMenu<TState, T>(GameStateMachine machine, T argument = default, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
			where TState : State<GameStateMachine>
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					case Debugger.Main:
						MenuScene.Load<TState, T>(machine, argument, loadSceneMode);
						break;
					case Debugger.Slava:
						MenuSceneSlava.Load<TState, T>(machine, argument, loadSceneMode);
						break;
					case Debugger.Dima:
						MenuSceneDima.Load<TState, T>(machine, argument, loadSceneMode);
						break;
					case Debugger.Soslan:
						MenuSceneSoslan.Load<TState, T>(machine, argument, loadSceneMode);
						break;
				}
			}
			else
			{
				MenuScene.Load<TState, T>(machine, argument, loadSceneMode);
			}
		}
		
		public void LoadFight<TState, T>(GameStateMachine machine, T argument = default, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
			where TState : State<GameStateMachine>
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					case Debugger.Main:
						FightScene.Load<TState, T>(machine, argument, loadSceneMode);
						break;
					case Debugger.Slava:
						FightSceneSlava.Load<TState, T>(machine, argument, loadSceneMode);
						break;
					case Debugger.Dima:
						FightSceneDima.Load<TState, T>(machine, argument, loadSceneMode);
						break;
					case Debugger.Soslan:
						FightSceneSoslan.Load<TState, T>(machine, argument, loadSceneMode);
						break;
				}
			}
			else
			{
				FightScene.Load<TState, T>(machine, argument, loadSceneMode);
			}
		}
	}
}