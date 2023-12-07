using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class Money : IntSave<Money>
	{
		public Money() =>
			Init(10000000);

		public Money(int value) =>
			Init(value);
		
		public override Money Clone() =>
			new Money(Value);

		private void Init(int value) =>
			Value = Mathf.Clamp(value, 0, int.MaxValue);
	}
}