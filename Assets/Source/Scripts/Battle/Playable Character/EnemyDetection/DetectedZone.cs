using ArenaHero.Data;
using System;
using System.Collections.Generic;
using ArenaHero.Utils.Object;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public class DetectedZone : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        public event Action<Enemy> EnemyDetected;
        
        public event Action<Enemy> EnemyLost;

        public Enemy TryGetEnemy() =>
            _enemies.Count != 0 
                ? _enemies[UnityEngine.Random.Range(0, _enemies.Count)] 
                : null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                _enemies.Add(enemy);

                enemy.Disabling += Lost;
                
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

        private void Lost(Enemy enemy)
        {
            enemy.Disabling -= Lost;
            _enemies.Remove(enemy);
            EnemyLost?.Invoke(enemy);
        }
    }
}

