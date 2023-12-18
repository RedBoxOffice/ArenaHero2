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
	[RequireComponent(typeof(UIFightSceneInitializer))]
	[RequireComponent(typeof(HeroInitializer))]
	public class LevelInitializer : MonoBehaviour, ISceneLoadHandlerOnState<GameStateMachine, LevelData>
	{
		[SerializeField] private WaveHandler _waveHandler;
		
		private NavMeshDataInstance _instanceNavMesh;
		private Action _unsubscribe;
		private LevelData _levelData;
		private RewardHandler _rewardHandler;

		public LevelData LevelData => _levelData;

		public RewardHandler RewardHandler => _rewardHandler;
		
		private void OnDisable()
		{
			_unsubscribe?.Invoke();
			NavMesh.RemoveNavMeshData(_instanceNavMesh);
		}
		
		public void OnSceneLoaded<TState>(GameStateMachine machine, LevelData argument = default)
			where TState : State<GameStateMachine>
		{
			GetComponent<WindowInitializer>().WindowsInit(machine.Window);
			GetComponent<UIFightSceneInitializer>().Init(machine);
			var heroInitializer = GetComponent<HeroInitializer>();
			_levelData = argument;
            
			Init(new Target(heroInitializer.Hero.transform, heroInitializer.Hero.gameObject.GetComponent<IDamageable>()));
		}
		
		private void Init(Target hero)
		{
			var currentStageIndex = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;
			var currentStage = _levelData.GetStageDataByIndex(currentStageIndex);
			
			var spawnerHandler = Instantiate(currentStage.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>(); 
			
			_rewardHandler = new RewardHandler();
			spawnerHandler.Spawned += _rewardHandler.OnSpawned;
			_unsubscribe = () => spawnerHandler.Spawned -= _rewardHandler.OnSpawned;
			
			spawnerHandler.Init(_waveHandler, hero);
			
			Instantiate(currentStage.EnvironmentParent);
		
			_instanceNavMesh = NavMesh.AddNavMeshData(currentStage.NavMeshData);
		}
	}
}