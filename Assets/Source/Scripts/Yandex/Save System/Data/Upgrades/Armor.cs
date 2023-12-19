using System;

namespace ArenaHero.Yandex.SaveSystem.Data
{
	[Serializable]
	public sealed class Armor : UpgradeSave<Armor>
	{
		protected override float MinValue { get; } = 0.01f;

		protected override float MaxValue { get; } = 0.45f;

		protected override float DefaultValue { get; } = 0.01f;

		public Armor() =>
			Init(DefaultValue, DefaultLevel);

		public Armor(float value, int level) =>
			Init(value, level);

		public override Armor Clone() =>
			new Armor(Value, Level);
	}
}