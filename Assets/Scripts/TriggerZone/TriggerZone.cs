using Game.Enemies;
using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        public Enemy TryGetEnemy()
        {

            return _enemies[Random.Range(0, _enemies.Count)];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {            
                _enemies.Add(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }
}

