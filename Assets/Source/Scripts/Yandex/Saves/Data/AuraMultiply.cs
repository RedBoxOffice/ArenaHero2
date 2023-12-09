using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class AuraMultiply : UpgradeSave<AuraMultiply>
	{
		private const float DefaultMultiply = 1f;
		private const int DefaultLevel = 1;
		
		public AuraMultiply() =>
			Init(DefaultMultiply, DefaultLevel);

		public AuraMultiply(float multiply, int level) =>
			Init(multiply, level);

		public override AuraMultiply Clone() =>
			new AuraMultiply(Multiply, Level);

		private void Init(float multiply, int level)
		{
			Multiply = Mathf.Clamp(multiply, 1f, 50f);
			Level = Mathf.Clamp(level, 1, 20);
		}
	}
}