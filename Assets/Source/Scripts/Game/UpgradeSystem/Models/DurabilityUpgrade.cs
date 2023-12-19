using System;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class DurabilityUpgrade : UpgradeModel<Durability>
	{
		protected override Durability Improve(float value, int level) =>
			new Durability(value, level);
	}
}