using System;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class HealthUpgrade : UpgradeModel<HealthMultiply>
	{
		protected override HealthMultiply Improve(HealthMultiply currentHealthMultiply)
		{
			var newHealthMultiply = new HealthMultiply(
				currentHealthMultiply.Multiply * MultiplyCoefficient,
				currentHealthMultiply.Level + 1);

			return newHealthMultiply;
		}
	}
}