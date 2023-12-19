using System;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class DamageUpgrade : UpgradeModel<Damage>
	{
		protected override Damage Improve(float value, int level) =>
			new Damage(value, level);
	}
}