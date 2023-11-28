using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class GameSavesData
	{
		[SerializeField] private CurrentLevel _currentLevel;
		[SerializeField] private CurrentLevelStage _currentLevelStage;

		public CurrentLevel CurrentLevel => _currentLevel ??= new CurrentLevel();
		
		public CurrentLevelStage CurrentLevelStage => _currentLevelStage ??= new CurrentLevelStage();
	}
}