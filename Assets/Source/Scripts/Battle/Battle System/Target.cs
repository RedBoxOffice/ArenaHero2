using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle
{
	public class Target
	{
		private Transform _transform;
		private IDamagable _damagable;
		private IReadOnlyCollection<IMover> _movers;

		public Transform Transform => _transform;
		
		public IDamagable Damagable => _damagable;
		
		public IReadOnlyCollection<IMover> Movers => _movers;

		public Target(Transform transform, IDamagable damagable, IReadOnlyCollection<IMover> movers)
		{
			_transform = transform;
			_damagable = damagable;
			_movers = movers;
		}
	}
}