using System;
using System.Collections.Generic;
using System.Linq;
using ArenaHero.Game.UpgradeSystem.Models;
using ArenaHero.Saves;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using Reflex.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArenaHero.Game.UpgradeSystem
{
	public class CharacteristicUpdater : MonoBehaviour, IModelHandler
	{
		[SerializeField] private ArmorUpgrade _armorUpgrade;
		[SerializeField] private AuraUpgrade _auraUpgrade;
		[SerializeField] private DamageUpgrade _damageUpgrade;
		[SerializeField] private DurabilityUpgrade _durabilityUpgrade;
		[SerializeField] private HealthUpgrade _healthUpgrade;
		[SerializeField] private LuckUpgrade _luckUpgrade;

		private Dictionary<Type, Improvement> _models;

		[Inject]
		private void Inject(ISaver saver)
		{
			foreach (var improvement in _models.Values)
			{
				improvement.Init(saver);
			}
		}
		
		private void Awake()
		{
			_models = new Dictionary<Type, Improvement>
			{
				[typeof(ArmorMultiply)] = _armorUpgrade,
				[typeof(AuraMultiply)] = _auraUpgrade,
				[typeof(DamageMultiply)] = _damageUpgrade,
				[typeof(DurabilityMultiply)] = _durabilityUpgrade,
				[typeof(HealthMultiply)] = _healthUpgrade,
				[typeof(LuckMultiply)] = _luckUpgrade,
			};
		}

		public void OnClick()
		{
			var index = Random.Range(0, _models.Count);
			
			_models.ElementAt(index).Value.TryUpdate();
		}

		public UpgradeModel<TMultiply> Get<TMultiply>()
			where TMultiply : UpgradeSave<TMultiply>
		{
			if (_models.ContainsKey(typeof(TMultiply)))
			{
				return (UpgradeModel<TMultiply>)_models[typeof(TMultiply)];
			}
			
			throw new ArgumentException();
		}
	}
}