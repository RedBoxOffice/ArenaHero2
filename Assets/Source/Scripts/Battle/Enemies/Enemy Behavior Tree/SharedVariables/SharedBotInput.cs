using System;
using BehaviorDesigner.Runtime;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	[Serializable]
	public class SharedBotInput : SharedVariable<BotInput>
	{
		public static implicit operator SharedBotInput(BotInput input) =>
			new SharedBotInput
			{
				Value = input
			};
	}
}