using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
	public class SetAttack : Action
	{
		[SerializeField] private SharedBotInput _botInput;

		public override TaskStatus OnUpdate()
		{
			_botInput.Value.CallAttack();
			
			return TaskStatus.Success;
		}
	}
}