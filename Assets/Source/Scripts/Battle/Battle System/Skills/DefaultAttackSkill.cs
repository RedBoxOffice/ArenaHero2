using System;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class DefaultAttackSkill : Skill
	{
		private float _damage = 10f;
		private IActionsInputHandler _inputHandler;

		public event Action<float, Action> TargetReach;
		public event Action<float, Character> AttackEnemy;

		private TargetChanger _targetChanger;

		[Inject]
		private void Inject(IActionsInputHandler actionsInputHandler, TargetChanger targetChanger)
		{
			_targetChanger = targetChanger;
			_inputHandler = actionsInputHandler;
			_inputHandler.Attack += Run;
		}

		public override void Run()
		{
			Attack();
		}

		private void Attack()
		{
			TargetReach?.Invoke(1, TryAttackEnemy);
		}

		private void TryAttackEnemy()
		{
			Debug.Log("Атака");
			AttackEnemy?.Invoke(_damage, null);

		}
	}
}