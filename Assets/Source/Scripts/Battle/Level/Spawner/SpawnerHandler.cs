using ArenaHero.Battle.PlayableCharacter;
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
        private ObjectSpawner<EnemyInit> _spawner;
        private Target _target;

        public void Init(WaveHandler waveHandler, Target hero)
        {
            _waveHandler = waveHandler;
            _target = hero;
            _waveHandler.Spawning += OnSpawning;
        }
        
        private void Awake()
        {
            _spawnPointsHandler = GetComponent<SpawnPointsHandler>();

            _spawner = new ObjectSpawner<EnemyInit>(
                new ObjectFactory<EnemyInit>(new GameObject(nameof(EnemyInit)).transform),
                new ObjectPool<EnemyInit>());
        }

        private void OnDisable()
        {
            if (_waveHandler != null)
                _waveHandler.Spawning -= OnSpawning;
        }

        private void OnSpawning(Enemy enemy)
        {
            var init = new EnemyInit()
            {
                Target = _target
            };

            _spawner.Spawn(enemy, init, () => _spawnPointsHandler.GetSpawnPosition());
        }
    }
}
