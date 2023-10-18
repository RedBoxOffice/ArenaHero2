using Base.Object;
using GameData;
using Reflex.Attributes;
using System;
using UnityEngine;

namespace Game.Enemies
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

        [Inject]
        private void Inject(WaveHandler waveHandler)
        {
            _waveHandler = waveHandler;
            _waveHandler.Spawn += OnSpawn;
        }

        private void OnSpawn(Enemy enemy)
        {
            //_spawner.Spawn(enemy, () => _spawnPointsHandler.GetSpawnPosition());
        }
    }
}
