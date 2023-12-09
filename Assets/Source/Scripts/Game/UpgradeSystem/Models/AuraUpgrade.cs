using System;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class AuraUpgrade : UpgradeModel<AuraMultiply>
	{
		protected override AuraMultiply Improve(AuraMultiply currentAuraMultiply)
		{
			var newAuraMultiply = new AuraMultiply(
				currentAuraMultiply.Multiply * MultiplyCoefficient,
				currentAuraMultiply.Level + 1);

			return newAuraMultiply;
		}
	}
}