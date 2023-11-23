using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArenaHero.Data
{
    [Serializable]
    public class WaveData
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private float _delayBetweenSpawns;
        [SerializeField] private int _countSpawns;

        public Enemy GetEnemyForSpawn => _enemies[Random.Range(0, _enemies.Length)];
        
        public float DelayBetweenSpawns => _delayBetweenSpawns;
        
        public int CountSpawns => _countSpawns;
    }
}