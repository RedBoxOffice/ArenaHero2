using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class DefaultAttackSkill : Skill, IAttackable
	{
		[SerializeField] private float _damage = 10f;
		[SerializeField] private float _attackDistance = 5f;
		[SerializeField] private Character _character;
		
		private IActionsInputHandler _inputHandler;
		private ITargetHandler _targetHandler;
		
		private Target Target => _targetHandler.Target;

		private void OnEnable()
		{
			if (_inputHandler != null)
				_inputHandler.Attack += Attack;
		}
		
		private void Start()
		{
			_targetHandler = _character.GetComponent<ITargetHandler>();
			_inputHandler = _character.GetComponent<IActionsInputHandler>();
			_inputHandler.Attack += Attack;
		}

		private void OnDisable() =>
			_inputHandler.Attack -= Attack;

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