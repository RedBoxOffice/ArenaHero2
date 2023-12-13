using System;
using ArenaHero.Yandex.Saves.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class ArmorUpgrade : UpgradeModel<Armor>
	{
		protected override Armor Improve(float value, int level) =>
			new Armor(value, level);
	}
}