using System;
using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace ArenaHero.Game.Level
{
	public class LevelInitializer
	{
		private NavMeshDataInstance _instanceNavMesh;
		private Action _unsubscribe;
		
		public void Init(LevelData levelData, WaveHandler waveHandler, Target hero)
		{
			var currentStageIndex = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;
			var currentStage = levelData.GetStageDataByIndex(currentStageIndex);
			
			var spawnerHandler = Object.Instantiate(currentStage.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>();
			var rewardHandler = new RewardHandler();

			spawnerHandler.Spawned += rewardHandler.OnSpawned;
			_unsubscribe = () => spawnerHandler.Spawned -= rewardHandler.OnSpawned;
			
			spawnerHandler.Init(waveHandler, hero);
			
			Object.Instantiate(currentStage.EnvironmentParent);
		
			_instanceNavMesh = NavMesh.AddNavMeshData(currentStage.NavMeshData);
		}

		public void Dispose()
		{
			_unsubscribe?.Invoke();
			NavMesh.RemoveNavMeshData(_instanceNavMesh);
		}
	}
}