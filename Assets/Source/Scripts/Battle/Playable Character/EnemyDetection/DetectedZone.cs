using System;
using System.Collections.Generic;
using ArenaHero.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public class DetectedZone : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        public event Action<Enemy> EnemyDetected;
        
        public event Action<Enemy> EnemyLost;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                _enemies.Add(enemy);

                enemy.Died += Lost;
                
                EnemyDetected?.Invoke(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                Lost(enemy);
            }
        }

        public Enemy TryGetEnemy() =>
            _enemies.Count != 0 
                ? _enemies[Random.Range(0, _enemies.Count)] 
                : null;
        
        private void Lost(Enemy enemy)
        {
            enemy.Died -= Lost;
            _enemies.Remove(enemy);
            EnemyLost?.Invoke(enemy);
        }
    }
}

