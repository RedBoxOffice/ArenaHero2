using System;
using System.Collections;
using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.Level
{
	public class WaveHandler : MonoBehaviour
	{
		private StageData _currentStageData;
		private WaveData _currentWaveData;

		private int _currentWaveIndex;
		private bool _isFight = true;

		public event Action<Enemy> Spawning;

		[Inject]
		private void Inject(LevelData levelData, IStateChangeable stateChangeable)
		{
			_currentStageData = levelData.GetStageDataByIndex(GameDataSaver.Instance.Get<CurrentLevelStage>().Value);
			_currentWaveData = _currentStageData.GetWaveDataByIndex(_currentWaveIndex);

			stateChangeable.StateChanged += (stateType) =>
			{
				if ((stateType is EndLevelState) is false)
				{
					return;
				}

				_isFight = false;
			};
		}

		public void Start() =>
			StartCoroutine(Fight());

		private IEnumerator Fight()
		{
			var waitBetweenWave = new WaitForSeconds(_currentStageData.DelayBetweenWave);
			var waitNextSpawn = new WaitForSeconds(_currentWaveData.DelayBetweenSpawns);

			yield return new WaitForSeconds(_currentStageData.StartDelay);

			while (_isFight)
			{
				for (var i = 0; i < _currentWaveData.CountSpawns; i++)
				{
					Spawning?.Invoke(_currentWaveData.GetEnemyForSpawn);

					yield return waitNextSpawn;
				}

				yield return waitBetweenWave;

				if (!TryChangeWave())
				{
					yield break;
				}
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