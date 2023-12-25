using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
	public class AttackCondition : Conditional
	{
		[SerializeField] private SharedAttackSkill _attack;
		[SerializeField] private SharedCharacter _character;
		
		private ITargetHolder _targetHolder;
		
		public override void OnAwake()
		{
			_targetHolder = _character.Value.GetComponent<ITargetHolder>();
		}
		
		public override TaskStatus OnUpdate()
		{
			if (Vector3.Distance(_targetHolder.Target.Transform.localPosition, transform.localPosition) <= _attack.Value.AttackDistance)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}