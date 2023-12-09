using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class DurabilityMultiply : UpgradeSave<DurabilityMultiply>
	{
		private const float DefaultMultiply = 1f;
		private const int DefaultLevel = 1;
		
		public DurabilityMultiply() =>
			Init(DefaultMultiply, DefaultLevel);

		public DurabilityMultiply(float multiply, int level) =>
			Init(multiply, level);

		public override DurabilityMultiply Clone() =>
			new DurabilityMultiply(Multiply, Level);

		private void Init(float multiply, int level)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
		}
	}
}