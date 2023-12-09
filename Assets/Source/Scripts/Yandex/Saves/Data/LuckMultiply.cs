using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class LuckMultiply : UpgradeSave<LuckMultiply>
	{
		private const float DefaultMultiply = 1f;
		private const int DefaultLevel = 1;
		
		public LuckMultiply() =>
			Init(DefaultMultiply, DefaultLevel);

		public LuckMultiply(float multiply, int level) =>
			Init(multiply, level);

		public override LuckMultiply Clone() =>
			new LuckMultiply(Multiply, Level);

		private void Init(float multiply, int level)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
		}
	}
}