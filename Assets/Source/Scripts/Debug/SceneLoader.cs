using System;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using UnityEngine;

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

		public string GetDebugFightSceneName()
		{
			switch (_debugger)
			{
				case Debugger.Main:
					return nameof(FightScene);
				case Debugger.Slava:
					return nameof(FightSceneSlava);
				case Debugger.Dima:
					return nameof(FightSceneDima);
				case Debugger.Soslan:
					return nameof(FightSceneSoslan);
			}

			return string.Empty;
		}

		public void LoadMenu<TState>(GameStateMachine machine)
			where TState : State<GameStateMachine>
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					case Debugger.Main:
						MenuScene.Load<TState>(machine);

						break;
					case Debugger.Slava:
						MenuSceneSlava.Load<TState>(machine);

						break;
					case Debugger.Dima:
						MenuSceneDima.Load<TState>(machine);

						break;
					case Debugger.Soslan:
						MenuSceneSoslan.Load<TState>(machine);

						break;
				}
			}
			else
			{
				MenuScene.Load<TState>(machine);
			}
		}

		public void LoadFight<TState, T>(GameStateMachine machine, T argument)
			where TState : State<GameStateMachine>
		{
			if (_isDebugMode)
			{
				switch (_debugger)
				{
					case Debugger.Main:
						FightScene.Load<TState, T>(machine, argument);

						break;
					case Debugger.Slava:
						FightSceneSlava.Load<TState, T>(machine, argument);

						break;
					case Debugger.Dima:
						FightSceneDima.Load<TState, T>(machine, argument);

						break;
					case Debugger.Soslan:
						FightSceneSoslan.Load<TState, T>(machine, argument);

						break;
				}
			}
			else
			{
				FightScene.Load<TState, T>(machine, argument);
			}
		}
	}
}