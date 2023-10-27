using ArenaHero.Data;
using ArenaHero.InputSystem;
using System;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public class TargetChanger : IDisposable
    {
        private DetectedZone _triggerZone;
        private LookTargetPoint _lookTargetPoint;
        private LookTargetPoint _defaultLookTargetPoint;
        private Enemy _currentEnemy;

        private IActionsInputHandler _actionsInputHandler;

        public event Action<Transform> TargetChanging;

        public TargetChanger(TargetChangerInject inject)
        {
            _triggerZone = inject.TriggerZone;
            _defaultLookTargetPoint = inject.LookTargetPoint;
            _lookTargetPoint = inject.LookTargetPoint;
            _actionsInputHandler = inject.ActionsInputHandler;

            _triggerZone.EnemyDetected += OnEnemyDetected;
            _triggerZone.EnemyLost += OnEnemyLost;
            _actionsInputHandler.ChangeTarget += OnChangeTarget;
        }

        public void Dispose()
        {
            _triggerZone.EnemyDetected -= OnEnemyDetected;
            _triggerZone.EnemyLost -= OnEnemyLost;
            _actionsInputHandler.ChangeTarget -= OnChangeTarget;
        }

        private void OnChangeTarget()
        {
            _currentEnemy = _triggerZone.TryGetEnemy();

            Transform newTarget;
            Vector3 newPosition;

            if (_currentEnemy != null)
            {
                newTarget = _currentEnemy.transform;
                _lookTargetPoint.transform.SetParent(newTarget);
                newPosition = Vector3.zero;
            }
            else
            {
                newTarget = _defaultLookTargetPoint.transform;
                _lookTargetPoint.transform.SetParent(null);
                newPosition = _defaultLookTargetPoint.transform.localPosition;
            }

            TargetChanging?.Invoke(newTarget);
            
            _lookTargetPoint.transform.localPosition = newPosition;
        }

        private void OnEnemyDetected(Enemy enemy)
        {
            if (_currentEnemy == null)
                OnChangeTarget();
        }

        private void OnEnemyLost(Enemy enemy)
        {
            if (_currentEnemy == null || _currentEnemy == enemy)
            {
                OnChangeTarget();
            }
        }
    }
}