using System;
using ArenaHero.Saves;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public sealed class Aura : UpgradeSave<Aura>
	{
		protected override float MaxValue { get; } = 200f;
        
		public Aura() =>
			Init(DefaultValue, DefaultLevel);

		public Aura(float value, int level) =>
			Init(value, level);

		public override Aura Clone() =>
			new Aura(Value, Level);
	}
}