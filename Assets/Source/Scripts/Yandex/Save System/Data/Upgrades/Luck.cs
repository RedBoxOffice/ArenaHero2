using System;

namespace ArenaHero.Yandex.SaveSystem.Data
{
	[Serializable]
	public sealed class Luck : UpgradeSave<Luck>
	{
		protected override float MaxValue { get; } = 3f;

		public Luck() =>
			Init(DefaultValue, DefaultLevel);

		public Luck(float value, int level) =>
			Init(value, level);

		public override Luck Clone() =>
			new Luck(Value, Level);
	}
}