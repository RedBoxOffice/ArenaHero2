using GameData;
using Reflex.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WaveHandler : MonoBehaviour
    {
        private LevelData _currentLevelData;
        private WaveData _currentWaveData;

        private int _currentWaveIndex = 0;

        public event Action<Enemy> Spawn;

        [Inject]
        private void Inject(LevelData levelData)
        {
            _currentLevelData = levelData;
            _currentWaveData = _currentLevelData.GetWaveData(_currentWaveIndex);
        }

        public void Start()
        {
            
        }

        //private IEnumerator Fight()
        //{
        //}
    }
}