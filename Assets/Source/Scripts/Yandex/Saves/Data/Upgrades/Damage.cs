using System;
using ArenaHero.Saves;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public sealed class Damage : UpgradeSave<Damage>
	{
		protected override float MinValue { get; } = 5f;
		
		protected override float MaxValue { get; } = 200f;

		protected override float DefaultValue { get; } = 5f;
		
		public Damage() =>
			Init(DefaultValue, DefaultLevel);

		public Damage(float value, int level) =>
			Init(value, level);

		public override Damage Clone() =>
			new Damage(Value, Level);
	}
}