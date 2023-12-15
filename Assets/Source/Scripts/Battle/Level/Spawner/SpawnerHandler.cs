using System;
using ArenaHero.Data;
using ArenaHero.Utils.Object;
using UnityEngine;

namespace ArenaHero.Battle.Level
{
    [RequireComponent(typeof(SpawnPointsHandler))]
    public class SpawnerHandler : MonoBehaviour
    {
        private WaveHandler _waveHandler;
        private SpawnPointsHandler _spawnPointsHandler;
        private ObjectSpawner<Enemy, EnemyInit> _spawner;
        private Target _target;

        public event Action<Enemy> Spawned;
        
        private void Awake()
        {
            _spawnPointsHandler = GetComponent<SpawnPointsHandler>();

            _spawner = new ObjectSpawner<Enemy, EnemyInit>(new GameObject(nameof(ObjectPool<Enemy, EnemyInit>)).transform);
        }

        private void OnDisable()
        {
            if (_waveHandler != null)
            {
                _waveHandler.Spawning -= OnSpawning;
            }
        }

        public void Init(WaveHandler waveHandler, Target hero)
        {
            _waveHandler = waveHandler;
            _target = hero;
            _waveHandler.Spawning += OnSpawning;
        }
        
        private void OnSpawning(Enemy enemy)
        {
            var init = new EnemyInit()
            {
                Target = _target
            };

            var poolingObject = _spawner.Spawn(enemy, init, () => _spawnPointsHandler.GetSpawnPosition());
            
            Spawned?.Invoke(poolingObject.Instance);
        }
    }
}
