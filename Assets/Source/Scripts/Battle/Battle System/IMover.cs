using System;
using UnityEngine;

namespace ArenaHero.Battle
{
	public interface IMover
	{
		public bool IsMoveLocked { get; }

		public bool TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget, out Action move);

		public void LockMove();

		public void UnlockMove();
	}
}