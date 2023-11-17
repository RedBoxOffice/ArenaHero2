using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class DefaultAttackSkill : Skill, IAttackable
	{
		[SerializeField] private float _damage = 10f;
		[SerializeField] private float _attackDistance = 5f;
		
		private IActionsInputHandler _inputHandler;
		private ITargetHandler _targetHandler;
		
		private Target Target => _targetHandler.Target;

		private void Awake()
		{
			_targetHandler = GetComponentInParent<ITargetHandler>();
			_inputHandler = GetComponentInParent<IActionsInputHandler>();
		}

		public void Attack() =>
			TryAttackEnemy();

		private void TryAttackEnemy()
		{
			if (CanAttack())
				Target.Damagable.TakeDamage(_damage);
		}

		private bool CanAttack() =>
			!(_attackDistance < Vector3.Distance(transform.position, Target.Transform.position));
	}
}