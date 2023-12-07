using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class DurabilityMultiply : UpgradeSave<DurabilityMultiply>
	{
		public DurabilityMultiply() =>
			Init(1f, 1, 50);

		public DurabilityMultiply(float multiply, int level, int price) =>
			Init(multiply, level, price);

		public override DurabilityMultiply Clone() =>
			new DurabilityMultiply(Multiply, Level, Price);

		private void Init(float multiply, int level, int price)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
			Price = Mathf.Clamp(price, 1, 50000);
		}
	}
}