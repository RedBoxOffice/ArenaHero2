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
		[SerializeField] private float _multiplyCoefficient;
		
		private ISaver _saver;

		public event Action<TUpgrade> Upgraded;

		protected float MultiplyCoefficient => _multiplyCoefficient;
		
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

			_saver.Set(new Money(SubtractCost(currentMoney, (int)currentPrice.Value)));
			currentPrice.Update();
			_saver.Set(currentPrice);

			var currentUpgrade = _saver.Get<TUpgrade>();
			var upgrade = Improve(currentUpgrade);
			_saver.Set(upgrade);
			
			Upgraded?.Invoke(upgrade);
		}

		protected abstract TUpgrade Improve(TUpgrade saver);
		
		private int SubtractCost(int currentMoney, int currentPrice) =>
			currentMoney - currentPrice;
	}
}