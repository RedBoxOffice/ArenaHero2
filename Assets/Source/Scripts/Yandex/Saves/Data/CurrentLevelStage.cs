using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public sealed class CurrentLevelStage : SimpleValueSave<int, CurrentLevelStage>
	{
		private const int DefaultValue = 0;
		
		public CurrentLevelStage() =>
			Init(DefaultValue);

		public CurrentLevelStage(int value) =>
			Init(value);
        
		public override CurrentLevelStage Clone() =>
			new CurrentLevelStage(Value);
		
		private void Init(int value) =>
			Value = Mathf.Clamp(value, 0, int.MaxValue);
	}
}