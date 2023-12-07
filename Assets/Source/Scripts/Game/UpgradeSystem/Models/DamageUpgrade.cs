using System;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class DamageUpgrade : UpgradeModel<DamageMultiply>
	{

		protected override DamageMultiply Improve(DamageMultiply currentDamageMultiply)
		{
			var newDamageMultiply = new DamageMultiply(
				currentDamageMultiply.Multiply * MultiplyCoefficient,
				currentDamageMultiply.Level + 1,
				(int)(currentDamageMultiply.Price * PriceCoefficient));

			return newDamageMultiply;
		}
	}
}