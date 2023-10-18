using UnityEngine;

namespace ArenaHero.Data
{
    [CreateAssetMenu(menuName = "Level/New Wave", fileName = "wave")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private Enemy[] _enemies;

        public Enemy GetEnemyForSpawn => _enemies[Random.Range(0, _enemies.Length)];
    }
}