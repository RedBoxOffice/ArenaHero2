using System;
using ArenaHero.Yandex.Saves;
using UnityEngine;

namespace ArenaHero.Saves
{
	[Serializable]
	public abstract class UpgradeSave<TData> : SaveData<TData> 
		where TData : UpgradeSave<TData>
	{
		[SerializeField] private float _value;
		[SerializeField] private int _level;
		
		public float Value { get => _value; protected set => _value = value; }
		
		public int Level { get => _level; protected set => _level = value; }

		protected virtual float MinValue { get; } = 1f;
		
		protected virtual float MaxValue { get; } = 2f;

		protected virtual int MinLevel { get; } = 1;
		
		protected virtual int MaxLevel { get; } = 20;

		protected virtual float DefaultValue { get; } = 1f;
		
		protected virtual int DefaultLevel { get; } = 1;

		public override abstract TData Clone();

		public float CalculateValue(int level)
		{
			var deltaLevel = MaxLevel - MinLevel;
			var deltaValue = MaxValue - MinValue;

			var normalLevel = level / deltaLevel;

			return MinValue + normalLevel * deltaValue;
		}

		public bool CanUpgrade() =>
			_level < MaxLevel;

		protected override void UpdateValue(TData data)
		{
			_value = data.Value;
			_level = data.Level;
		}

		protected override bool Equals(TData data) =>
			data.Value.Equals(_value)
			&& data.Level.Equals(_level);
		
		protected void Init(float value, int level)
		{
			Value = Mathf.Clamp(value, MinValue, MaxValue);
			Level = Mathf.Clamp(level, MinLevel, MaxLevel);
		}
	}
}