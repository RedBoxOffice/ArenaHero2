using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
	public class CurrentLevelStage : SaveData<CurrentLevelStage>
	{
		[SerializeField] private int _index;

		public int Index => _index;
		
		public CurrentLevelStage() =>
			_index = 0;

		public CurrentLevelStage(int index) =>
			_index = Mathf.Clamp(index, 0, int.MaxValue);

		public override CurrentLevelStage Clone() =>
			new CurrentLevelStage(_index);
		
		protected override void UpdateValue(CurrentLevelStage value) =>
			_index = value.Index;

		protected override bool Equals(CurrentLevelStage value) =>
			value.Index.Equals(_index);
	}
}