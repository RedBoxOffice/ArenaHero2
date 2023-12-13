using System;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArenaHero.Yandex.Saves
{
	[Serializable]
	public class GameSaves
	{
		[SerializeField] private CurrentLevel _currentLevel;
		[SerializeField] private CurrentLevelStage _currentLevelStage;
		[SerializeField] private Money _money;
		[SerializeField] private Crystals _crystals;
		[FormerlySerializedAs("_armorMultiply")]
		[SerializeField] private Armor _armor;
		[FormerlySerializedAs("_auraMultiply")]
		[SerializeField] private Aura _aura;
		[FormerlySerializedAs("_damageMultiply")]
		[SerializeField] private Damage _damage;
		[FormerlySerializedAs("_durabilityMultiply")]
		[SerializeField] private Durability _durability;
		[FormerlySerializedAs("_healthMultiply")]
		[SerializeField] private Health _health;
		[FormerlySerializedAs("_luckMultiply")]
		[SerializeField] private Luck _luck;
		[SerializeField] private CurrentUpgradePrice _currentUpgradePrice;
 
		public CurrentLevel CurrentLevel => _currentLevel ??= new CurrentLevel();
		
		public CurrentLevelStage CurrentLevelStage => _currentLevelStage ??= new CurrentLevelStage();

		public Money Money => _money ??= new Money();

		public Crystals Crystals => _crystals ??= new Crystals();
		
		public Armor Armor => _armor ??= new Armor();
		
		public Aura Aura => _aura ??= new Aura();
		
		public Damage Damage => _damage ??= new Damage();
		
		public Durability Durability => _durability ??= new Durability();
		
		public Health Health => _health ??= new Health();
		
		public Luck Luck => _luck ??= new Luck();

		public CurrentUpgradePrice CurrentUpgradePrice => _currentUpgradePrice ??= new CurrentUpgradePrice();
	}
}