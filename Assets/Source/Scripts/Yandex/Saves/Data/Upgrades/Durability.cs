using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public sealed class Durability : UpgradeSave<Durability>
	{
		protected override float MinValue { get; } = 0.01f;
		
		protected override float MaxValue { get; } = 0.45f;

		protected override float DefaultValue { get; } = 0.01f;
		
		public Durability() =>
			Init(DefaultValue, DefaultLevel);

		public Durability(float value, int level) =>
			Init(value, level);

		public override Durability Clone() =>
			new Durability(Value, Level);
	}
}