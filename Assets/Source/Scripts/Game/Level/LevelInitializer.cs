using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Game.Level
{
	public class LevelInitializer
	{
		private NavMeshDataInstance _instanceNavMesh;
		
		public LevelInitializer(LevelData levelData, WaveHandler waveHandler, Target hero)
		{
			var spawnerHandler = Object.Instantiate(levelData.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>();
			
			spawnerHandler.Init(waveHandler, hero);
			
			Object.Instantiate(levelData.EnvironmentParent);
		
			_instanceNavMesh = NavMesh.AddNavMeshData(levelData.NavMeshData);
		}

		public void Dispose() =>
			NavMesh.RemoveNavMeshData(_instanceNavMesh);
	}
}