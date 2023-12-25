using System;

namespace ArenaHero.Battle.CharacteristicHolders
{
	public abstract class Feature
	{
		private float _value;
		
		public float Value
		{
			get => _value;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				_value = value;
			}
		}

		protected Feature(float value) =>
			Value = value;
	}
}