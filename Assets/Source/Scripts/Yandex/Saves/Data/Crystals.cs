using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class Crystals : IntSave<Crystals>
	{
		public Crystals() =>
			Init(20);
		
		public Crystals(int value) =>
			Init(value);

		public override Crystals Clone() =>
			new Crystals(Value);

		private void Init(int value) =>
			Value = Mathf.Clamp(value, 0, int.MaxValue);
	}
}