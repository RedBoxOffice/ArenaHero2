using System;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class PlayerData
	{
		public CurrentLevel CurrentLevel = new CurrentLevel(0);
	}
}