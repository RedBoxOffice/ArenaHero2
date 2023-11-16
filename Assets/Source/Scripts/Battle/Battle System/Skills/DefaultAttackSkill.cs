using System;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Data;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class DefaultAttackSkill : Skill, IAttackable
	{
		[SerializeField] private float _damage = 10f;
		[SerializeField] private float _attackDistance = 5f;
		
		private IActionsInputHandler _inputHandler;
		private ITargetHandler _targetHandler;
		
		private Transform Target => _targetHandler.Target;

		private void Awake()
		{
			_targetHandler = GetComponentInParent<ITargetHandler>();
			_inputHandler = GetComponentInParent<IActionsInputHandler>();
		}

		public void Attack() =>
			TryAttackEnemy();

		private void TryAttackEnemy()
		{
			if (!Target.gameObject.transform.parent.gameObject.TryGetComponent(out ICharacter character))
				return;
			
			if (CanAttack(character))
				character.TakeDamage(_damage);
		}

		private bool CanAttack(ICharacter character)
		{
			if (character.Type != TriggerCharacterType)
				return false;

			return !(_attackDistance < Vector3.Distance(transform.position, character.Position));
		}
	}
}