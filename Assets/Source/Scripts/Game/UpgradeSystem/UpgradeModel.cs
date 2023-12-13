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
		private ISaver _saver;
		
		public event Action<TUpgrade> Upgraded;

		public override void Init(ISaver saver) =>
			_saver = saver;

		public override void TryUpdate()
		{
			var currentMoney = _saver.Get<Money>().Value;
			var currentPrice = _saver.Get<CurrentUpgradePrice>();
			
			if (currentMoney < currentPrice.Value)
			{
				return;
			}

			var currentUpgrade = _saver.Get<TUpgrade>();

			if (currentUpgrade.CanUpgrade() is false)
			{
				return;
			}
			
			_saver.Set(new Money(SubtractCost(currentMoney, (int)currentPrice.Value)));
			currentPrice.Update();
			_saver.Set(currentPrice);

			var newLevel = currentUpgrade.Level + 1;
			
			var upgrade = Improve(currentUpgrade.CalculateValue(newLevel), newLevel);
			
			_saver.Set(upgrade);
			
			Upgraded?.Invoke(upgrade);
		}

		protected abstract TUpgrade Improve(float value, int level);
		
		private int SubtractCost(int currentMoney, int currentPrice) =>
			currentMoney - currentPrice;
	}
}