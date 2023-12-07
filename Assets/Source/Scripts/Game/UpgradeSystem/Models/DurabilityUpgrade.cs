using System;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class DurabilityUpgrade : UpgradeModel<DurabilityMultiply>
	{

		protected override DurabilityMultiply Improve(DurabilityMultiply currentDurabilityMultiply)
		{
			var newDurabilityMultiply = new DurabilityMultiply(
				currentDurabilityMultiply.Multiply * MultiplyCoefficient,
				currentDurabilityMultiply.Level + 1,
				(int)(currentDurabilityMultiply.Price * PriceCoefficient));

			return newDurabilityMultiply;
		}
	}
}