using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class Money : SimpleValueSave<int, Money>
	{
		private const int DefaultValue = 0;
		
		public Money() =>
			Init(DefaultValue);

		public Money(int value) =>
			Init(value);
		
		public override Money Clone() =>
			new Money(Value);

		private void Init(int value) =>
			Value = Mathf.Clamp(value, 0, int.MaxValue);
	}
}