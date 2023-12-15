using System;
using ArenaHero.Battle;
using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.Utils.Object;
using UnityEngine;

namespace ArenaHero.Data
{
	public abstract class Enemy : MonoBehaviour, IPoolingObject<Enemy, EnemyInit>, ITargetHolder, IHealthHolder, IArmorHolder, IDurabilityHolder, IAuraHolder, IDamageHolder
	{
		[SerializeField] private float _health;
		[SerializeField] private float _armor;
		[SerializeField] private float _durability;
		[SerializeField] private float _aura;
		[SerializeField] private float _damage;

		private Character _character;

		public event Action<Enemy> Disabling;

		public event Action<IPoolingObject<Enemy, EnemyInit>> Disabled;

		public float Health => _health;

		public float Armor => _armor;

		public float Durability => _durability;

		public float Aura => _aura;

		public float Damage => _damage;

		public Enemy Instance => this;

		public IDamageable SelfDamageable => _character;

		public Target Target { get; private set; }

		public abstract Type SelfType { get; }

		private void Awake() =>
			_character = GetComponent<Character>();

		private void OnEnable() =>
			_character.Died += OnDied;

		private void OnDisable()
		{
			Disabled?.Invoke(this);
			_character.Died -= OnDied;
		}

		public void Init(EnemyInit init) =>
			Target = init.Target;

		private void OnDied()
		{
			Disabling?.Invoke(this);
			gameObject.SetActive(false);
		}
	}
}