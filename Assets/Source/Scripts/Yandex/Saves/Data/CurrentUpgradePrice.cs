using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public sealed class CurrentUpgradePrice : SimpleValueSave<float, CurrentUpgradePrice>
	{
		private const float DefaultValue = 100;
		private const float ValueUpdateMultiplier = 1.1f;
        
		public CurrentUpgradePrice() =>
			Init(DefaultValue);

		public CurrentUpgradePrice(float value) =>
			Init(value);
        
		public override CurrentUpgradePrice Clone() =>
			new CurrentUpgradePrice(Value);

		public void Update() =>
			Value *= ValueUpdateMultiplier;
		
		private void Init(float value) =>
			Value = Mathf.Clamp(value, DefaultValue, float.MaxValue);
	}
}