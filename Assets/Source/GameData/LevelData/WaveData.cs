using UnityEngine;

namespace ArenaHero.Data
{
    [CreateAssetMenu(menuName = "Level/New Wave", fileName = "wave")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private float _delayBetweenSpawns;
        [SerializeField] private int _countSpawns;

        public Enemy GetEnemyForSpawn => _enemies[Random.Range(0, _enemies.Length)];
        public float DelayBetweenSpawns => _delayBetweenSpawns;
        public int CountSpawns => _countSpawns;
    }
}