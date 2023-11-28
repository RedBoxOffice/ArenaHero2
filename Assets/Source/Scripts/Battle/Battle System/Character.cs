﻿using System;
using Source.GameData.Characters;
using UnityEngine;

namespace ArenaHero.Battle
{
    [RequireComponent(typeof(HealthView))]
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private CharacterData _data;

        public event Action Died;
        
        public event Action<float> HealthChanged;
        
        public CharacterData Data => _data;
        
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _currentHealth = _data.MaxHealth;

            HealthChanged += _ =>
            {
                if (_currentHealth <= 0)
                {
                    Died?.Invoke();
                }
            };
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            HealthChanged?.Invoke(_currentHealth);
        }
    }
}
