using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Game.Level
{
	public class LevelInitializer
	{
		private NavMeshDataInstance _instanceNavMesh;
		
		public void Init(LevelData levelData, WaveHandler waveHandler, Target hero)
		{
			var currentStageIndex = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;
			var currentStage = levelData.GetStageDataByIndex(currentStageIndex);
			
			var spawnerHandler = Object.Instantiate(currentStage.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>();
			
			spawnerHandler.Init(waveHandler, hero);
			
			Object.Instantiate(currentStage.EnvironmentParent);
		
			_instanceNavMesh = NavMesh.AddNavMeshData(currentStage.NavMeshData);
		}

		public void Dispose() =>
			NavMesh.RemoveNavMeshData(_instanceNavMesh);
	}
}