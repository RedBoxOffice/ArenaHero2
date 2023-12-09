using System;
using ArenaHero.Utils.Other;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
	[Serializable]
	public class HorizontalMoveSettings
	{
		[SerializeField] private FloatRange _distanceRange;
		[SerializeField] private FloatRange _radiusRange;
		[SerializeField] private AnimationCurve _curve;

		public FloatRange DistanceRange => _distanceRange;
		
		public float DistanceDelta => _distanceRange.Max - _distanceRange.Min;
		
		public float RadiusDelta => _radiusRange.Max - _radiusRange.Min;
		
		public float GetNormalValue(float distanceBetweenSelfAndTarget)
		{
			var clampedRadius = Mathf.Clamp(distanceBetweenSelfAndTarget, _radiusRange.Min, _radiusRange.Max);
			var normalRadius = (clampedRadius - _radiusRange.Min) / RadiusDelta;
            
			return _curve.Evaluate(normalRadius);
		}
	}
}