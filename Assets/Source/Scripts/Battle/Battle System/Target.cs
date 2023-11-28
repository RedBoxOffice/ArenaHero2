using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle
{
	public class Target
	{
		private Transform _transform;
		private IDamageable _damageable;

		public Transform Transform => _transform;
		
		public IDamageable Damageable => _damageable;

		public Target(Transform transform, IDamageable damageable)
		{
			_transform = transform;
			_damageable = damageable;
		}
	}
}