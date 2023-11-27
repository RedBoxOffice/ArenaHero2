using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class GameSavesData
	{
		[SerializeField] private CurrentLevel _currentLevel;
		[SerializeField] private MaxHealth _maxHealth;

		public CurrentLevel CurrentLevel => _currentLevel ??= new CurrentLevel();
		
		public MaxHealth MaxHealth => _maxHealth ??= new MaxHealth();
	}
}