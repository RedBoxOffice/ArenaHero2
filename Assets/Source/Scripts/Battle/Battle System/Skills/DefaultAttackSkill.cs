using System;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Data;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class DefaultAttackSkill : Skill
	{
		private float _damage = 10f;
		private IActionsInputHandler _inputHandler;

		private LookTargetPoint _lookTargetPoint;

		public event Action<float, Action> TargetReach;
		public event Action<float, Character> AttackEnemy;

		[Inject]
		private void Inject(IActionsInputHandler actionsInputHandler, LookTargetPoint lookTargetPoint)
		{
			_lookTargetPoint = lookTargetPoint;
			_inputHandler = actionsInputHandler;
			_inputHandler.Attack += Run;
		}

		public override void Run()
		{
			Debug.Log("Mouse");
			Attack();
		}

		private void Attack()
		{
			TargetReach?.Invoke(1, TryAttackEnemy);
		}

		private void TryAttackEnemy()
		{
			Debug.Log("Атака");

			Enemy enemy = _lookTargetPoint.gameObject.GetComponentInParent<Enemy>();
			Character character = enemy.gameObject.GetComponent<Character>();
			AttackEnemy?.Invoke(_damage, character);

		}
	}
}