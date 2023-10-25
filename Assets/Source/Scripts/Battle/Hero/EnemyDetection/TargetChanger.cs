using ArenaHero.Data;
using ArenaHero.InputSystem;
using System;
using UnityEngine;

namespace ArenaHero.Fight.Player.EnemyDetection
{
    public class TargetChanger : IDisposable
    {
        private TriggerZone _triggerZone;
        private LookTargetPoint _lookTargetPoint;
        private LookTargetPoint _defaultLookTargetPoint;
        private Enemy _currentEnemy;

        private IActionsInputHandler _actionsInputHandler;

        public TargetChanger(TargetChangerInject inject)
        {
            _triggerZone = inject.TriggerZone;
            _defaultLookTargetPoint = inject.LookTargetPoint;
            _lookTargetPoint = inject.LookTargetPoint;
            _actionsInputHandler = inject.ActionsInputHandler;

            _triggerZone.EnemyDetected += OnEnemyDetected;
            _triggerZone.EnemyLost += OnEnemyLost;
            _actionsInputHandler.ChangeTarget += PreferEnemy;
        }

        private void PreferEnemy()
        {
            _currentEnemy = _triggerZone.TryGetEnemy();

            if (_currentEnemy != null)
            {
                _lookTargetPoint.transform.SetParent(_currentEnemy.transform);
                _lookTargetPoint.transform.localPosition = Vector3.zero;
            }
            else
            {
                _lookTargetPoint.transform.SetParent(null);
                _lookTargetPoint.transform.localPosition = _defaultLookTargetPoint.transform.localPosition;
            }
        }

        public void Dispose()
        {
            _triggerZone.EnemyDetected -= OnEnemyDetected;
            _triggerZone.EnemyLost -= OnEnemyLost;
            _actionsInputHandler.ChangeTarget -= PreferEnemy;
        }

        private void OnEnemyDetected(Enemy enemy)
        {
            if (_currentEnemy == null)
                PreferEnemy();
        }

        private void OnEnemyLost(Enemy enemy)
        {
            if (_currentEnemy == null || _currentEnemy == enemy)
            {
                PreferEnemy();
            }
        }
    }
}