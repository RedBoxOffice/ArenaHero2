using UnityEngine;

namespace ArenaHero.Battle
{
	public class Target
	{
		private Transform _transform;
		private IDamagable _damagable;

		public Transform Transform => _transform;
		public IDamagable Damagable => _damagable;

		public Target(Transform transform, IDamagable damagable)
		{
			_transform = transform;
			_damagable = damagable;
		}
	}
}