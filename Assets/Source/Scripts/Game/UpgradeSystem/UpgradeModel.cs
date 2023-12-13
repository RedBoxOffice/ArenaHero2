using System;
using ArenaHero.Saves;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;

namespace ArenaHero.Game.UpgradeSystem
{
	[Serializable]
	public abstract class UpgradeModel<TUpgrade> : Improvement
		where TUpgrade : UpgradeSave<TUpgrade>
	{
		public event Action<TUpgrade> Upgraded;

		public override void TryUpdate()
		{
			var currentMoney = GameDataSaver.Instance.Get<Money>().Value;
			var currentPrice = GameDataSaver.Instance.Get<CurrentUpgradePrice>();
			
			if (currentMoney < currentPrice.Value)
			{
				return;
			}

			var currentUpgrade = GameDataSaver.Instance.Get<TUpgrade>();

			if (currentUpgrade.CanUpgrade() is false)
			{
				return;
			}
			
			GameDataSaver.Instance.Set(new Money(SubtractCost(currentMoney, (int)currentPrice.Value)));
			currentPrice.Update();
			GameDataSaver.Instance.Set(currentPrice);

			var newLevel = currentUpgrade.Level + 1;
			
			var upgrade = Improve(currentUpgrade.CalculateValue(newLevel), newLevel);
			
			GameDataSaver.Instance.Set(upgrade);
			
			Upgraded?.Invoke(upgrade);
		}

		protected abstract TUpgrade Improve(float value, int level);
		
		private int SubtractCost(int currentMoney, int currentPrice) =>
			currentMoney - currentPrice;
	}
}