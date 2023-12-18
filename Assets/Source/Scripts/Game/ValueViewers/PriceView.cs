using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.ValueViewers
{
	public class PriceView : SaveValueView<float, CurrentUpgradePrice>
	{
		protected override string GetOutputFormatted(CurrentUpgradePrice value) =>
			"Цена: " + (int)value.Value;
	}
}