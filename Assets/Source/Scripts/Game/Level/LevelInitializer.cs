using System;
using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Game.Level
{
	[RequireComponent(typeof(WindowInitializer))]
	[RequireComponent(typeof(FightSceneTransitionInitializer))]
	[RequireComponent(typeof(PlayerInitializer))]
	public class LevelInitializer : MonoBehaviour, ISceneLoadHandlerOnStateAndArgument<GameStateMachine, LevelData>
	{
		[SerializeField] private WaveHandler _waveHandler;
		
		private NavMeshDataInstance _instanceNavMesh;
		private Action _unsubscribe;
		private LevelData _levelData;
		private RewardHandler _rewardHandler;
		private EndLevelHandler _endLevelHandler;

		public LevelData LevelData => _levelData;

		public RewardHandler RewardHandler => _rewardHandler;
		
		private void OnDisable()
		{
			_unsubscribe?.Invoke();
			NavMesh.RemoveNavMeshData(_instanceNavMesh);
		}
		
		public void OnSceneLoaded<TState>(GameStateMachine machine, LevelData levelData)
			where TState : State<GameStateMachine>
		{
			GetComponent<WindowInitializer>().WindowsInit(machine.Window);
			
			var playerInitializer = GetComponent<PlayerInitializer>();
			
			_levelData = levelData;
            
			Init(new Target(playerInitializer.GetHero().transform, playerInitializer.GetHero().gameObject.GetComponent<IDamageable>()));
			
			GetComponent<FightSceneTransitionInitializer>().Init(machine, _levelData, playerInitializer.GetHero(), _endLevelHandler);
			
			machine.EnterIn<TState>();
		}
		
		private void Init(Target hero)
		{
			var currentStageIndex = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;
			var currentStage = _levelData.GetStageDataByIndex(currentStageIndex);
			
			var spawnerHandler = Instantiate(currentStage.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>(); 
			
			_rewardHandler = new RewardHandler();
			_endLevelHandler = new EndLevelHandler();
			
			spawnerHandler.Spawned += _rewardHandler.OnSpawned;
			spawnerHandler.Spawned += _endLevelHandler.OnSpawned;
			_waveHandler.WavesEnded += _endLevelHandler.OnWavesEnded;
			
			_unsubscribe = () =>
			{
				spawnerHandler.Spawned -= _rewardHandler.OnSpawned;
				spawnerHandler.Spawned -= _endLevelHandler.OnSpawned;
				_waveHandler.WavesEnded -= _endLevelHandler.OnWavesEnded;
			};
			
			spawnerHandler.Init(_waveHandler, hero);
			
			Instantiate(currentStage.EnvironmentParent);
		
			_instanceNavMesh = NavMesh.AddNavMeshData(currentStage.NavMeshData);
		}
	}
}