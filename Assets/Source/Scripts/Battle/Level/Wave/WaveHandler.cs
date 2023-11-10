using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using Reflex.Attributes;
using System;
using System.Collections;
using UnityEngine;

namespace ArenaHero.Battle.Level
{
	public class WaveHandler : MonoBehaviour
	{
		private LevelData _currentLevelData;
		private WaveData _currentWaveData;

		private int _currentWaveIndex = 0;
		private bool _isFight = true;

		[Inject]
		private void Inject(LevelData levelData, IEndLevelStateChanged endLevel)
		{
			_currentLevelData = levelData;
			_currentWaveData = _currentLevelData.GetWaveData(_currentWaveIndex);
			endLevel.StateChanged += () => _isFight = false;
		}

		public event Action<Enemy> Spawning;

		public void Start() =>
			StartCoroutine(Fight());

		private IEnumerator Fight()
		{
			var waitNextSpawn = new WaitForSeconds(_currentWaveData.DelayBetweenSpawns);

			while (_isFight)
			{
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

			if (_currentWaveIndex >= _currentLevelData.WaveCount)
				return false;
			
			_currentWaveData = _currentLevelData.GetWaveData(_currentWaveIndex);
			
			return true;
		}
	}
}