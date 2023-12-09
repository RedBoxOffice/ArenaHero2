using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class ArmorMultiply : UpgradeSave<ArmorMultiply>
	{
		private const float DefaultMultiply = 1f;
		private const int DefaultLevel = 1;
		
		public ArmorMultiply() =>
			Init(DefaultMultiply, DefaultLevel);

		public ArmorMultiply(float multiply, int level) =>
			Init(multiply, level);

		public override ArmorMultiply Clone() =>
			new ArmorMultiply(Multiply, Level);

		private void Init(float multiply, int level)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
		}
	}
}