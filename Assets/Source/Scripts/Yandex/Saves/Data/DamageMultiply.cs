using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class DamageMultiply : UpgradeSave<DamageMultiply>
	{
		private const float DefaultMultiply = 1f;
		private const int DefaultLevel = 1;
		
		public DamageMultiply() =>
			Init(DefaultMultiply, DefaultLevel);

		public DamageMultiply(float multiply, int level) =>
			Init(multiply, level);

		public override DamageMultiply Clone() =>
			new DamageMultiply(Multiply, Level);

		private void Init(float multiply, int level)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
		}
	}
}