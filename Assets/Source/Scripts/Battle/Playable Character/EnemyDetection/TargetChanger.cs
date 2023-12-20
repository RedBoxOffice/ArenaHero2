using ArenaHero.Data;
using ArenaHero.InputSystem;
using System;
using ArenaHero.Utils.Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public class TargetChanger : IDisposable
    {
        private readonly DetectedZone _triggerZone;
        private readonly LookTargetPoint _lookTargetPoint;
        private readonly IActionsInputHandlerOnlyPlayer _actionsInputHandler;

        private Enemy _currentEnemy;
        private Enemy _previousEnemy;

        public TargetChanger(DetectedZone detectedZone, LookTargetPoint lookTargetPoint, IActionsInputHandlerOnlyPlayer actionsInputHandler)
        {
            _triggerZone = detectedZone;
            _lookTargetPoint = lookTargetPoint;
            _actionsInputHandler = actionsInputHandler;

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
            _previousEnemy = null;
            UpdateCurrentEnemy();
            
            SetParentForLookTargetPoint();
            _lookTargetPoint.UpdateTarget(GetTarget());
            _lookTargetPoint.transform.localPosition = GetTargetPointPosition();
        }

        private void UpdateCurrentEnemy(int countRepeat = 0)
        {
            const int maxCountRepeat = 10;
            
            _previousEnemy = _currentEnemy;
            _currentEnemy = _triggerZone.TryGetEnemy();

            if (_currentEnemy is null)
            {
                return;
            }

            if (countRepeat > maxCountRepeat)
            {
                return;
            }
            
            if (_previousEnemy == _currentEnemy)
            {
                UpdateCurrentEnemy(++countRepeat);
            }
        }

        private void SetParentForLookTargetPoint()
        {
            if (_currentEnemy is not null)
            {
                _lookTargetPoint.transform.SetParent(_currentEnemy.transform);

                return;
            }
            
            _lookTargetPoint.transform.SetParent(null);
            _lookTargetPoint.gameObject.MoveGameObjectToActiveFightScene();
        }
        
        private Vector3 GetTargetPointPosition() =>
            _currentEnemy is null 
                ? _lookTargetPoint.transform.localPosition 
                : Vector3.zero;

        private Target GetTarget() =>
            _currentEnemy is null
                ? new Target(_lookTargetPoint.transform, null)
                : new Target(_currentEnemy.transform, _currentEnemy.SelfDamageable);
        
        private void OnEnemyDetected(Enemy enemy)
        {
            if (_currentEnemy is null)
            {
                OnChangeTarget();
            }
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