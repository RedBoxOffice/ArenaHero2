using ArenaHero.Data;
using ArenaHero.InputSystem;
using System;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public class TargetChanger : IDisposable
    {
        private readonly DetectedZone _triggerZone;
        private readonly LookTargetPoint _lookTargetPoint;
        private readonly LookTargetPoint _defaultLookTargetPoint;
        private readonly IActionsInputHandlerOnlyPlayer _actionsInputHandler;

        private Enemy _currentEnemy;

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
            
            OnChangeTarget();
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

            Transform newEnemy;
            Target newTarget;
            Vector3 newPosition;

            if (_currentEnemy != null)
            {
                newEnemy = _currentEnemy.transform;
                _lookTargetPoint.transform.SetParent(newEnemy);
                newPosition = Vector3.zero;
                newTarget = new Target(_currentEnemy.transform, _currentEnemy.SelfDamagable, _currentEnemy.Movers);
            }
            else
            {
                newEnemy = _defaultLookTargetPoint.transform;
                _lookTargetPoint.transform.SetParent(null);
                newPosition = _defaultLookTargetPoint.transform.localPosition;
                newTarget = new Target(_defaultLookTargetPoint.transform, null, null);
            }
            
            TargetChanging?.Invoke(newEnemy);
            
            _lookTargetPoint.UpdateTarget(newTarget);
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