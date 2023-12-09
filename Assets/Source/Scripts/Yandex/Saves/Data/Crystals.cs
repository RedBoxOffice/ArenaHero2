using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class Crystals : IntSave<Crystals>
	{
		private const int DefaultValue = 20;
		
		public Crystals() =>
			Init(DefaultValue);
		
		public Crystals(int value) =>
			Init(value);

		public override Crystals Clone() =>
			new Crystals(Value);

		private void Init(int value) =>
			Value = Mathf.Clamp(value, 0, int.MaxValue);
	}
}