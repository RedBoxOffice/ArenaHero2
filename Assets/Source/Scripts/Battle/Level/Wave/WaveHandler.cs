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
		private Coroutine _fightCoroutine;

		private int _currentWaveIndex;
		private bool _isWaveWorking = true;

		public event Action WavesEnded;
		
		public event Action<Enemy> Spawning;

		[Inject]
		private void Inject(LevelData levelData, IStateChangeable stateChangeable)
		{
			_currentStageData = levelData.GetStageDataByIndex(GameDataSaver.Instance.Get<CurrentLevelStage>().Value);
			_currentWaveData = _currentStageData.GetWaveDataByIndex(_currentWaveIndex);

			StartFight();

			stateChangeable.StateChanged += (stateType) =>
			{
				if (stateType == typeof(EndLevelState))
				{
					_isWaveWorking = false;
				}

				if (stateType == typeof(FightState))
				{
					StartFight();
				}
			};
		}

		private void StartFight()
		{
			if (_fightCoroutine != null)
			{
				StopCoroutine(_fightCoroutine);
			}
			
			_fightCoroutine = StartCoroutine(Fight());
		}

		private IEnumerator Fight()
		{
			_isWaveWorking = true;
			
			var waitBetweenWave = new WaitForSeconds(_currentStageData.DelayBetweenWave);
			var waitNextSpawn = new WaitForSeconds(_currentWaveData.DelayBetweenSpawns);

			yield return new WaitForSeconds(_currentStageData.StartDelay);

			while (_isWaveWorking)
			{
				for (var i = 0; i < _currentWaveData.CountSpawns; i++)
				{
					Spawning?.Invoke(_currentWaveData.GetEnemyForSpawn);

					yield return waitNextSpawn;
				}

				yield return waitBetweenWave;

				if (!TryChangeWave())
				{
					_isWaveWorking = false;
				}
			}
			
			WavesEnded?.Invoke();
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