using System;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;

namespace ArenaHero.Yandex.Saves
{
	[Serializable]
	public class GameSaves
	{
		[SerializeField] private CurrentLevel _currentLevel;
		[SerializeField] private CurrentLevelStage _currentLevelStage;
		[SerializeField] private Money _money;
		[SerializeField] private Crystals _crystals;
		[SerializeField] private ArmorMultiply _armorMultiply;
		[SerializeField] private AuraMultiply _auraMultiply;
		[SerializeField] private DamageMultiply _damageMultiply;
		[SerializeField] private DurabilityMultiply _durabilityMultiply;
		[SerializeField] private HealthMultiply _healthMultiply;
		[SerializeField] private LuckMultiply _luckMultiply;
		[SerializeField] private CurrentUpgradePrice _currentUpgradePrice;
 
		public CurrentLevel CurrentLevel => _currentLevel ??= new CurrentLevel();
		
		public CurrentLevelStage CurrentLevelStage => _currentLevelStage ??= new CurrentLevelStage();

		public Money Money => _money ??= new Money();

		public Crystals Crystals => _crystals ??= new Crystals();
		
		public ArmorMultiply ArmorMultiply => _armorMultiply ??= new ArmorMultiply();
		
		public AuraMultiply AuraMultiply => _auraMultiply ??= new AuraMultiply();
		
		public DamageMultiply DamageMultiply => _damageMultiply ??= new DamageMultiply();
		
		public DurabilityMultiply DurabilityMultiply => _durabilityMultiply ??= new DurabilityMultiply();
		
		public HealthMultiply HealthMultiply => _healthMultiply ??= new HealthMultiply();
		
		public LuckMultiply LuckMultiply => _luckMultiply ??= new LuckMultiply();

		public CurrentUpgradePrice CurrentUpgradePrice => _currentUpgradePrice ??= new CurrentUpgradePrice();
	}
}