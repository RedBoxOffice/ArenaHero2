using System;
using BehaviorDesigner.Runtime;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	[Serializable]
	public class SharedCharacter : SharedVariable<Character>
	{
		public static implicit operator SharedCharacter(Character input) =>
			new SharedCharacter
			{
				Value = input
			};
	}
}