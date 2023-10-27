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

        public event Action<Enemy> Spawn;

        [Inject]
        private void Inject(LevelData levelData, IOverFight over)
        {
            _currentLevelData = levelData;
            _currentWaveData = _currentLevelData.GetWaveData(_currentWaveIndex);
            over.Over += () => _isFight = false;
        }

        public void Start()
        {
            StartCoroutine(Fight());
        }

        private IEnumerator Fight()
        {
            var waitNextSpawn = new WaitForSeconds(_currentWaveData.DelayBetweenSpawns);

            while (_isFight)
            {
                for (int i = 0; i < _currentWaveData.CountSpawns; i++)
                {
                    Spawn?.Invoke(_currentWaveData.GetEnemyForSpawn);

                    yield return waitNextSpawn;
                }

                if (!TryChangeWave())
                    _isFight = false;
            }
        }

        private bool TryChangeWave()
        {
            _currentWaveIndex++;

            if (_currentWaveIndex < _currentLevelData.WaveCount)
            {
                _currentWaveData = _currentLevelData.GetWaveData(_currentWaveIndex);
                return true;
            }
            
            return false;
        }
    }
}