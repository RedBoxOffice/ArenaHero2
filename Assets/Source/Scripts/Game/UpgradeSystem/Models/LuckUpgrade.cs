using System;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class LuckUpgrade : UpgradeModel<LuckMultiply>
	{
		protected override LuckMultiply Improve(LuckMultiply currentLuckMultiply)
		{
			var newLuckMultiply = new LuckMultiply(
				currentLuckMultiply.Multiply * MultiplyCoefficient,
				currentLuckMultiply.Level + 1);

			return newLuckMultiply;
		}
	}
}