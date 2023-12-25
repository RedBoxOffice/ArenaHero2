using System;
using System.Collections.Generic;
using ArenaHero.Battle;
using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.Utils.Object;
using UnityEngine;

namespace ArenaHero.Data
{
	public abstract class Enemy : MonoBehaviour, IPoolingObject<Enemy, EnemyInit>, ITargetHolder, IFeatureHolder
	{
		[SerializeField] private int _rewardMoney;
		[SerializeField] private int _rewardScore;
		
		[SerializeField] private float _health;
		[SerializeField] private float _armor;
		[SerializeField] private float _durability;
		[SerializeField] private float _aura;
		[SerializeField] private float _damage;

		private Character _character;
		private Dictionary<Type, Feature> _features;

		public event Action<Enemy> Died;

		public event Action<IPoolingObject<Enemy, EnemyInit>> Disabled;

		public int RewardMoney => _rewardMoney;

		public int RewardScore => _rewardScore;

		public Enemy Instance => this;

		public IDamageable SelfDamageable => _character;

		public Target Target { get; private set; }

		public abstract Type SelfType { get; }

		private void Awake()
		{
			_character = GetComponentInChildren<Character>();

			_features = new Dictionary<Type, Feature>
			{
				[typeof(HealthFeature)] = new HealthFeature(_health),
				[typeof(ArmorFeature)] = new ArmorFeature(_armor),
				[typeof(DamageFeature)] = new DamageFeature(_durability),
				[typeof(DurabilityFeature)] = new DurabilityFeature(_aura),
				[typeof(AuraFeature)] = new AuraFeature(_damage),
			};
		}

		private void OnEnable() =>
			_character.Died += OnDied;

		private void OnDisable()
		{
			Disabled?.Invoke(this);
			_character.Died -= OnDied;
		}

		public float Get<TFeature>()
		{
			if (_features.ContainsKey(typeof(TFeature)))
			{
				return _features[typeof(TFeature)].Value;
			}

			throw new KeyNotFoundException(nameof(TFeature));
		}

		public void Set<TFeature>(float value)
		{
			if (_features.ContainsKey(typeof(TFeature)))
			{
				_features[typeof(TFeature)].Value = value;
			}
            
			throw new KeyNotFoundException(nameof(TFeature));
		}
		
		public void Init(EnemyInit init) =>
			Target = init.Target;

		private void OnDied()
		{
			Died?.Invoke(this);
			gameObject.SetActive(false);
		}
	}
}