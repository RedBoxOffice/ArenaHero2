using System;
using ArenaHero.Data;
using BehaviorDesigner.Runtime;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	[Serializable]
	public class SharedEnemy : SharedVariable<Enemy>
	{
		public static implicit operator SharedEnemy(Enemy value) =>
			new SharedEnemy
			{
				Value = value
			};
	}
}