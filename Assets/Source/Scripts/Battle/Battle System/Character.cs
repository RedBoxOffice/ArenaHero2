using System;
using ArenaHero.Battle.CharacteristicHolders;
using UnityEngine;

namespace ArenaHero.Battle
{
	public class Character : MonoBehaviour, IDamageable
	{
		[SerializeField] private float _currentHealth;

		private IFeatureHolder _featureHolder;
		
		public event Action Died;

		public event Action<float> HealthChanged;

		public float MaxHealth => _featureHolder.Get<HealthFeature>();
		
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

		private void OnEnable()
		{
			if (TryGetComponent(out IFeatureHolder featureHolder) || transform.parent.TryGetComponent(out featureHolder))
			{
				_featureHolder = featureHolder;
				_currentHealth = MaxHealth;
			}
			else
			{
				throw new NullReferenceException(nameof(IFeatureHolder));
			}
		}

		public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			HealthChanged?.Invoke(_currentHealth);
		}

		public void AddHealth(float newHealth)
		{			
			_currentHealth += newHealth;
			HealthChanged?.Invoke(_currentHealth);
		}
	}
}