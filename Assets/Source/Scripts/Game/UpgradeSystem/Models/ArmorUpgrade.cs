using System;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class ArmorUpgrade : UpgradeModel<ArmorMultiply>
	{
		protected override ArmorMultiply Improve(ArmorMultiply currentArmorMultiply)
		{
			var newArmorMultiply = new ArmorMultiply(
				currentArmorMultiply.Multiply * MultiplyCoefficient,
				currentArmorMultiply.Level + 1);

			return newArmorMultiply;
		}
	}
}