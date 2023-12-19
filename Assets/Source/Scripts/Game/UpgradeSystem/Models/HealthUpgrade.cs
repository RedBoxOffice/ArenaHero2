using System;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class HealthUpgrade : UpgradeModel<Health>
	{
		protected override Health Improve(float value, int level) =>
			new Health(value, level);
	}
}