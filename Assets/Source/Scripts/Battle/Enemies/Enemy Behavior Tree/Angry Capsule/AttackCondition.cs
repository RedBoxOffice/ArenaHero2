using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
	public class AttackCondition : Conditional
	{
		public SharedFloat _attackDistance;
		public MonoBehaviour _targetHandlerBehaviour;

		private ITargetHandler _targetHandler;
		
		public override void OnAwake()
		{
			if (_targetHandlerBehaviour && _targetHandlerBehaviour is not ITargetHandler)
			{
				Debug.LogError(nameof(_targetHandlerBehaviour) + " needs to implement " + nameof(ITargetHandler));
				_targetHandlerBehaviour = null;
			}
			else
			{
				_targetHandler = (ITargetHandler)_targetHandlerBehaviour;
			}
		}
		
		public override TaskStatus OnUpdate()
		{
			if ((Vector3.Distance(_targetHandler.Target.Transform.position, transform.position)) <= _attackDistance.Value)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}