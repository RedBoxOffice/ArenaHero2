using ArenaHero.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public class DetectedZone : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        public bool ContainEnemy => _enemies.Count != 0;

        public event Action<Enemy> EnemyDetected;
        public event Action<Enemy> EnemyLost;

        public Enemy TryGetEnemy() =>
            _enemies.Count != 0 ? _enemies[UnityEngine.Random.Range(0, _enemies.Count)] : null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                _enemies.Add(enemy);
                EnemyDetected?.Invoke(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemies.Remove(enemy);
                EnemyLost?.Invoke(enemy);
            }
        }
    }
}

