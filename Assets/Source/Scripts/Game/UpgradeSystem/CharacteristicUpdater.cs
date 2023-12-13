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
				[typeof(Armor)] = new ArmorUpgrade(),
				[typeof(Aura)] = new AuraUpgrade(),
				[typeof(Damage)] = new DamageUpgrade(),
				[typeof(Durability)] = new DurabilityUpgrade(),
				[typeof(Health)] = new HealthUpgrade(),
				[typeof(Luck)] = new LuckUpgrade(),
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