using System;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.Game.UpgradeSystem
{
	[Serializable]
	public abstract class UpgradeModel<TUpgrade> : Improvement
		where TUpgrade : UpgradeSave<TUpgrade>, new()
	{
		public event Action<TUpgrade> Upgraded;
		
		public override bool TryImprove()
		{
			var currentMoney = GameDataSaver.Instance.Get<Money>().Value;
			var currentPrice = GameDataSaver.Instance.Get<CurrentUpgradePrice>();
			
			if (currentMoney < currentPrice.Value)
			{
				return true;
			}

			var currentUpgrade = GameDataSaver.Instance.Get<TUpgrade>();

			if (currentUpgrade.CanUpgrade() is false)
			{
				return false;
			}

			var money = currentMoney - (int)currentPrice.Value;
			
			GameDataSaver.Instance.Set(new Money(money));
			currentPrice.Update();
			GameDataSaver.Instance.Set(currentPrice);

			var newLevel = currentUpgrade.Level + 1;
			
			var upgrade = Improve(currentUpgrade.CalculateValue(newLevel), newLevel);
			
			GameDataSaver.Instance.Set(upgrade);
			
			Upgraded?.Invoke(upgrade);

			return true;
		}

		protected abstract TUpgrade Improve(float value, int level);
	}
}