using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves
{
	[Serializable]
	public abstract class IntSave<TData> : SaveData<TData> where TData : IntSave<TData>
	{
		[SerializeField] private int _value;

		public int Value { get => _value; protected set => _value = value; }

		public override abstract TData Clone();

		protected override void UpdateValue(TData value) =>
			_value = value.Value;
		
		protected override bool Equals(TData value) =>
			value.Value.Equals(_value);
	}
}