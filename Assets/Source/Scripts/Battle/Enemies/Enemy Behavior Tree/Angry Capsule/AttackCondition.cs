using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
	public class AttackCondition : Conditional
	{
		public SharedCharacter Character;
		
		private ITargetHandler _targetHandler;
		
		public override void OnAwake()
		{
			_targetHandler = Character.Value.GetComponent<ITargetHandler>();
		}
		
		public override TaskStatus OnUpdate()
		{
			if ((Vector3.Distance(_targetHandler.Target.Transform.position, transform.position)) <= Character.Value.Data.AttackDistance)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}