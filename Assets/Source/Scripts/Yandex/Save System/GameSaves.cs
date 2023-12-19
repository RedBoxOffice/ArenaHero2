using System;
using System.Reflection;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;

namespace ArenaHero.Yandex.SaveSystem
{
	[Serializable]
	public class GameSaves
	{
		[SerializeField] private CurrentLevel _currentLevel;
		[SerializeField] private CurrentLevelStage _currentLevelStage;
		[SerializeField] private Money _money;
		[SerializeField] private Crystals _crystals;
		[SerializeField] private Armor _armor;
		[SerializeField] private Aura _aura;
		[SerializeField] private Damage _damage;
		[SerializeField] private Durability _durability;
		[SerializeField] private Health _health;
		[SerializeField] private Luck _luck;
		[SerializeField] private CurrentUpgradePrice _currentUpgradePrice;
		
		public TData Get<TData>()
			where TData : SaveData<TData>, new()
		{
			var type = GetType();
			
			var fields = type.GetFields(
				BindingFlags.NonPublic
				| BindingFlags.Instance
				| BindingFlags.DeclaredOnly
				| BindingFlags.GetField);
			
			foreach (var field in fields)
			{
				if (field.FieldType == typeof(TData))
				{
					var value = field.GetValue(this);
					
					if (value == null)
					{
						field.SetValue(this, new TData());
						value = field.GetValue(this);
					}

					return (TData)value;
				}
			}

			throw new ArgumentNullException($"{typeof(TData)} Not contains");
		}
	}
}