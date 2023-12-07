using System;
using ArenaHero.Yandex.Saves;

namespace ArenaHero.Game.UpgradeSystem
{
	[Serializable]
	public abstract class Improvement
	{
		public abstract void Init(ISaver saver);
		
		public abstract void TryUpdate();
	}
}