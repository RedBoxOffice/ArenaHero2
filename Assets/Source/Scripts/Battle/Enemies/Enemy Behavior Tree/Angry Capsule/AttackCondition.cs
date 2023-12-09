using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
	public class AttackCondition : Conditional
	{
		[SerializeField] private SharedCharacter _character;
		
		private ITargetHandler _targetHandler;
		
		public override void OnAwake()
		{
			_targetHandler = _character.Value.GetComponent<ITargetHandler>();
		}
		
		public override TaskStatus OnUpdate()
		{
			if ((Vector3.Distance(_targetHandler.Target.Transform.position, transform.position)) <= _character.Value.Data.AttackDistance)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}