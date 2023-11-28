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
        private readonly IActionsInputHandlerOnlyPlayer _actionsInputHandler;

        private Enemy _currentEnemy;

        public event Action<Transform> TargetChanging;

        public TargetChanger(TargetChangerInject inject)
        {
            _triggerZone = inject.TriggerZone;
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
            
            TargetChanging?.Invoke(GetTargetTransform());
            _lookTargetPoint.transform.SetParent(GetParentForLookTargetPoint());
            _lookTargetPoint.UpdateTarget(GetTarget());
            _lookTargetPoint.transform.localPosition = GetTargetPointPosition();
        }
        
        private Transform GetTargetTransform() =>
            _currentEnemy is null 
                ? _lookTargetPoint.transform 
                : _currentEnemy.transform;

        private Transform GetParentForLookTargetPoint() =>
            _currentEnemy is null 
                ? null 
                : _currentEnemy.transform;

        private Vector3 GetTargetPointPosition() =>
            _currentEnemy is null 
                ? _lookTargetPoint.transform.localPosition 
                : Vector3.zero;

        private Target GetTarget() =>
            _currentEnemy is null
                ? new Target(_lookTargetPoint.transform, null)
                : new Target(_currentEnemy.transform, _currentEnemy.SelfDamagable);
        
        private void OnEnemyDetected(Enemy enemy)
        {
            if (_currentEnemy is null)
                OnChangeTarget();
        }

        private void OnEnemyLost(Enemy enemy)
        {
            if (_currentEnemy is null || _currentEnemy.Equals(enemy))
            {
                OnChangeTarget();
            }
        }
    }
}