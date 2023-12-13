using System;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class DurabilityUpgrade : UpgradeModel<Durability>
	{
		protected override Durability Improve(float value, int level) =>
			new Durability(value, level);
	}
}