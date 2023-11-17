using System;
using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace ArenaHero.Game.Level
{
	public class LevelInitializer : IDisposable
	{
		private NavMeshDataInstance _instance;
		
		public LevelInitializer(LevelData levelData, WaveHandler waveHandler, Target hero)
		{
			var spawnerHandler = Object.Instantiate(levelData.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>();
			
			spawnerHandler.Init(waveHandler, hero);
			
			Object.Instantiate(levelData.EnvironmentParent);
		
			_instance = NavMesh.AddNavMeshData(levelData.NavMeshData);
		}
			
		public void Dispose() =>
			_instance.Remove();
	}
}