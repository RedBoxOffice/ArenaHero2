using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	public class MaxHealth : SaveData
	{
		[SerializeField] private float _value;

		public float Value => _value;

		public MaxHealth() =>
			_value = 100;

		public MaxHealth(float value) =>
			_value = Mathf.Clamp(value, 0, float.MaxValue);
		
		public override event Action<SaveData> ValueUpdated;

		public override void UpdateValue<TData>(TData value, Action successCallback)
		{
			if (value is MaxHealth health)
			{
				_value = health.Value;
				successCallback();
				ValueUpdated?.Invoke(Clone());
			}
		}

		public override SaveData Clone() =>
			new MaxHealth(_value);
	}
}