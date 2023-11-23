using System;
using UnityEngine;

namespace ArenaHero.Battle
{
	public interface IMover
	{
		public void TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget);
	}
}