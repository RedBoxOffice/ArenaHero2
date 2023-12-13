using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public sealed class Health : UpgradeSave<Health>
	{
		protected override float MinValue { get; } = 100f;
		
		protected override float MaxValue { get; } = 5000f;

		protected override float DefaultValue { get; } = 100f;
		
		public Health() =>
			Init(DefaultValue, DefaultLevel);

		public Health(float value, int level) =>
			Init(value, level);

		public override Health Clone() =>
			new Health(Value, Level);
	}
}