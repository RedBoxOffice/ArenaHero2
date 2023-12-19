using System;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class LuckUpgrade : UpgradeModel<Luck>
	{
		protected override Luck Improve(float value, int level) =>
			new Luck(value, level);
	}
}