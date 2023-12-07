using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Game.Level
{
	public class LevelInitializer : MonoBehaviour
	{
		private const int NeedProgressValue = 2;
		
		private int _selfInitProgress = 0;
		private NavMeshDataInstance _instanceNavMesh;
		private LevelData _levelData;
		private WaveHandler _waveHandler;
		private Target _hero;
		private ISaver _saver;

		[Inject]
		private void Inject(ISaver saver)
		{
			_saver = saver;
			
			TryInitialize();
		}
		
		public void Init(LevelData levelData, WaveHandler waveHandler, Target hero)
		{
			_levelData = levelData;
			_waveHandler = waveHandler;
			_hero = hero;
			
			TryInitialize();
		}

		public void Dispose() =>
			NavMesh.RemoveNavMeshData(_instanceNavMesh);

		private void TryInitialize()
		{
			_selfInitProgress++;

			if (_selfInitProgress.Equals(NeedProgressValue))
			{
				InitializeLevel();
			}
		}
		
		private void InitializeLevel()
		{
			var currentStageIndex = _saver.Get<CurrentLevelStage>().Value;
			var currentStage = _levelData.GetStageDataByIndex(currentStageIndex);
			
			var spawnerHandler = Object.Instantiate(currentStage.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>();
			
			spawnerHandler.Init(_waveHandler, _hero);
			
			Object.Instantiate(currentStage.EnvironmentParent);
		
			_instanceNavMesh = NavMesh.AddNavMeshData(currentStage.NavMeshData);
		}
	}
}