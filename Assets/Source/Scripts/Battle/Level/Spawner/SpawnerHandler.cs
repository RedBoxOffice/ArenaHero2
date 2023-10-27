using ArenaHero.Data;
using ArenaHero.Battle.Level;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Utils.Object;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.Level
{
    [RequireComponent(typeof(SpawnPointsHandler))]
    public class SpawnerHandler : MonoBehaviour
    {
        private WaveHandler _waveHandler;
        private SpawnPointsHandler _spawnPointsHandler;
        private ObjectSpawner<EnemyInit> _spawner;
        private Hero _target;

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
        private void Inject(WaveHandler waveHandler, Hero hero)
        {
            _waveHandler = waveHandler;
            _target = hero;
            _waveHandler.Spawn += OnSpawn;
        }

        private void OnSpawn(Enemy enemy)
        {
            var init = new EnemyInit()
            {
                Target = _target.transform
            };

            _spawner.Spawn(enemy, init, () => _spawnPointsHandler.GetSpawnPosition());
        }
    }
}
