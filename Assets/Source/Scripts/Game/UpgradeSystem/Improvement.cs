using System;

namespace ArenaHero.Game.UpgradeSystem
{
	[Serializable]
	public abstract class Improvement
	{
		public abstract bool TryImprove();
	}
}