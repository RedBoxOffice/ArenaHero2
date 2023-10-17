using Codice.Client.Common.GameUI;
using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyChanger : IDisposable
    {
        private TriggerZone _triggerZone;
        private Transform _lookAtPoint;
        private Transform _defaultLookPoint;
        private Enemy _currentEnemy;

        public EnemyChanger(TriggerZone triggerZone, Transform lookAtPoint)
        {           
            _triggerZone = triggerZone;
            _defaultLookPoint = lookAtPoint;
            _lookAtPoint = lookAtPoint;
            triggerZone.EnemyLeaved += OnEnemyLeaved;
        }

        public void OnEnemyLeaved(Enemy enemy)
        {
            if (_currentEnemy == enemy)
            {
                PreferEnemy();
            }
        }

        public void PreferEnemy()
        {
            _currentEnemy = _triggerZone.TryGetEnemy();

            if (_currentEnemy != null)
                _lookAtPoint.position = _currentEnemy.transform.position;
            else
                _lookAtPoint.position = _defaultLookPoint.position;
        }      

        public void Dispose()
        {
            _triggerZone.EnemyLeaved -= OnEnemyLeaved;
        }
    }
}

