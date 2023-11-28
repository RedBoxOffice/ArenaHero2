using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class GameSavesData
	{
		[SerializeField] private CurrentLevel _currentLevel;

		public CurrentLevel CurrentLevel => _currentLevel ??= new CurrentLevel();
	}
}