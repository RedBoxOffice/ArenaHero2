using System;
using ArenaHero.Battle.CharacteristicHolders;
using UnityEngine;

namespace ArenaHero.Battle
{
	public class Character : MonoBehaviour, IDamageable
	{
		[SerializeField] private float _currentHealth;

		public event Action Died;

		public event Action<float> HealthChanged;

		public float CurrentHealth => _currentHealth;

		private void Awake()
		{
			HealthChanged += _ =>
			{
				if (_currentHealth <= 0)
				{
					Died?.Invoke();
				}
			};
		}

		private void OnEnable() =>
			_currentHealth = GetComponent<IHealthHolder>().Health;

		public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			HealthChanged?.Invoke(_currentHealth);
		}
	}
}