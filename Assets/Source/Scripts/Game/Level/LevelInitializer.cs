using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	[RequireComponent(typeof(WindowInitializer))]
	[RequireComponent(typeof(FightSceneTransitionInitializer))]
	[RequireComponent(typeof(PlayerInitializer))]
	public class LevelInitializer : MonoBehaviour, ISceneLoadHandlerOnStateAndArgument<GameStateMachine, LevelData>
	{
		[SerializeField] private WaveHandler _waveHandler;
		
		private LevelData _levelData;
		private LevelStageChanger _levelStageChanger;

		public LevelData LevelData => _levelData;

		public RewardHandler RewardHandler => _levelStageChanger.RewardHandler;

		private EndLevelHandler EndLevelHandler => _levelStageChanger.EndLevelHandler;
		
		private void OnDisable() =>
			_levelStageChanger.Dispose();

		public void OnSceneLoaded<TState>(GameStateMachine machine, LevelData levelData)
			where TState : State<GameStateMachine>
		{
			GetComponent<WindowInitializer>().WindowsInit(machine.Window);
			
			var playerInitializer = GetComponent<PlayerInitializer>();
			
			_levelData = levelData;

			_levelStageChanger = new LevelStageChanger(
				_waveHandler,
				levelData,
				new Target(playerInitializer.GetHero().transform, playerInitializer.GetHero().gameObject.GetComponent<IDamageable>()),
				this);
			
			_levelStageChanger.StageChanged += playerInitializer.OnStageChanged;
			
			_levelStageChanger.ChangeStage();
			
			GetComponent<FightSceneTransitionInitializer>().Init(machine, playerInitializer.GetHero(), EndLevelHandler, _levelStageChanger);

			
			machine.EnterIn<TState>();
		}
	}
}