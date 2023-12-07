using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using Reflex.Attributes;
using System;
using System.Collections;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;

namespace ArenaHero.Battle.Level
{
	public class WaveHandler : MonoBehaviour
	{
		private LevelData _currentLevelData;
		private StageData _currentStageData;
		private WaveData _currentWaveData;

		private int _currentWaveIndex = 0;
		private bool _isFight = true;

		[Inject]
		private void Inject(ISaver saver, LevelData levelData, IEndLevelStateChanged endLevel)
		{
			_currentLevelData = levelData;
			_currentStageData = levelData.GetStageDataByIndex(saver.Get<CurrentLevelStage>().Value);
			_currentWaveData = _currentStageData.GetWaveDataByIndex(_currentWaveIndex);
			
			endLevel.StateChanged += () => _isFight = false;
		}

		public event Action<Enemy> Spawning;

		public void Start() =>
			StartCoroutine(Fight());

		private IEnumerator Fight()
		{
			var waitBetweenWave = new WaitForSeconds(_currentStageData.DelayBetweenWave);
			var waitNextSpawn = new WaitForSeconds(_currentWaveData.DelayBetweenSpawns);

			yield return new WaitForSeconds(_currentStageData.StartDelay);
			
			while (_isFight)
			{
				yield return waitBetweenWave;
				
				for (var i = 0; i < _currentWaveData.CountSpawns; i++)
				{
					Spawning?.Invoke(_currentWaveData.GetEnemyForSpawn);

					yield return waitNextSpawn;
				}

				if (!TryChangeWave())
					_isFight = false;
			}
		}

		private bool TryChangeWave()
		{
			_currentWaveIndex++;

			if (_currentWaveIndex >= _currentStageData.WaveCount)
				return false;
			
			_currentWaveData = _currentStageData.GetWaveDataByIndex(_currentWaveIndex);
			
			return true;
		}
	}
}