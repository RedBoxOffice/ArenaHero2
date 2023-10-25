using ArenaHero.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Fight.Player.EnemyDetection
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
        
        public event Action<Enemy> EnemyDetected;
        public event Action<Enemy> EnemyLost;

        public Enemy TryGetEnemy()
        {
            if (_enemies.Count != 0)
            {
                return _enemies[UnityEngine.Random.Range(0, _enemies.Count)];
            }
            else
            {
                return null;
            }
        }

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

