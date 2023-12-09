using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class HealthMultiply : UpgradeSave<HealthMultiply>
	{
		private const float DefaultMultiply = 1f;
		private const int DefaultLevel = 1;
		
		public HealthMultiply() =>
			Init(DefaultMultiply, DefaultLevel);

		public HealthMultiply(float multiply, int level) =>
			Init(multiply, level);

		public override HealthMultiply Clone() =>
			new HealthMultiply(Multiply, Level);

		private void Init(float multiply, int level)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
		}
	}
}