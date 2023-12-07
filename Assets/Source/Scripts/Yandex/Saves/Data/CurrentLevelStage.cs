using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	[Serializable]
	public class CurrentLevelStage : IntSave<CurrentLevelStage>
	{
		public CurrentLevelStage() =>
			Init(0);

		public CurrentLevelStage(int value) =>
			Init(value);
        
		public override CurrentLevelStage Clone() =>
			new CurrentLevelStage(Value);
		
		private void Init(int value) =>
			Value = Mathf.Clamp(value, 0, int.MaxValue);
	}
}