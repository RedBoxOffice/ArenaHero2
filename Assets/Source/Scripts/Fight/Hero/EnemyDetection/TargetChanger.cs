using ArenaHero.Data;
using System;
using UnityEngine;

namespace ArenaHero.Fight.Hero.EnemyDetection
{
    public class TargetChanger : IDisposable
    {
        private TriggerZone _triggerZone;
        private Transform _lookTargetPoint;
        private Transform _defaultLookTargetPoint;
        private Enemy _currentEnemy;

        public TargetChanger(TriggerZone triggerZone, Transform lookTargetPoint)
        {
            _triggerZone = triggerZone;
            _defaultLookTargetPoint = lookTargetPoint;
            _lookTargetPoint = lookTargetPoint;
            triggerZone.EnemyLeaved += OnEnemyLeaved;
        }

        public void PreferEnemy()
        {
            _currentEnemy = _triggerZone.TryGetEnemy();

            if (_currentEnemy != null)
            {
                _lookTargetPoint.SetParent(_currentEnemy.transform);
                _lookTargetPoint.position = Vector3.zero;
            }
            else
            {
                _lookTargetPoint.SetParent(null);
                _lookTargetPoint.position = _defaultLookTargetPoint.position;
            }
        }

        public void Dispose()
        {
            _triggerZone.EnemyLeaved -= OnEnemyLeaved;
        }

        private void OnEnemyLeaved(Enemy enemy)
        {
            if (_currentEnemy == enemy)
            {
                PreferEnemy();
            }
        }
    }
}