using UnityEngine;

namespace ArenaHero.Battle
{
	public class Target
	{
		public Target(Transform transform, IDamageable damageable)
		{
			Transform = transform;
			Damageable = damageable;
		}
		
		public Transform Transform { get; }

		public IDamageable Damageable { get; }
	}
}