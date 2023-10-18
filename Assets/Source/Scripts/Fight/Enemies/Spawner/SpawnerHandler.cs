using ArenaHero.Data;
using ArenaHero.Fight.Level;
using ArenaHero.Utils.Object;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Fight.Enemies.Spawner
{
    [RequireComponent(typeof(SpawnPointsHandler))]
    public class SpawnerHandler : MonoBehaviour
    {
        private WaveHandler _waveHandler;
        private SpawnPointsHandler _spawnPointsHandler;
        private ObjectSpawner<EnemyInit> _spawner;

        private void Awake()
        {
            _spawnPointsHandler = GetComponent<SpawnPointsHandler>();

            _spawner = new(
                new ObjectFactory<EnemyInit>(new GameObject(nameof(EnemyInit)).transform),
                new ObjectPool<EnemyInit>());
        }

        private void OnDisable()
        {
            if (_waveHandler != null)
                _waveHandler.Spawn -= OnSpawn;
        }

        [Inject]
        private void Inject(WaveHandler waveHandler)
        {
            _waveHandler = waveHandler;
            _waveHandler.Spawn += OnSpawn;
        }

        private void OnSpawn(Enemy enemy)
        {
            _spawner.Spawn(enemy, () => _spawnPointsHandler.GetSpawnPosition());
        }
    }
}
