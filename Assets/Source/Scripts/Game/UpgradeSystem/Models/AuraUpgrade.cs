using System;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.UpgradeSystem.Models
{
	[Serializable]
	public class AuraUpgrade : UpgradeModel<Aura>
	{
		protected override Aura Improve(float value, int level) =>
			new Aura(value, level);
	}
}