using System;
using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class JerkForAttackSkill : AttackSkill
	{
		[SerializeField] private float _timeToTarget;
		
		protected override void OnAttack() =>
			TryJerk();

		private void TryJerk()
		{
			if (CanJerk())
			{
				Vector3 direction = Vector3.forward;
				float distance = Vector3.Distance(transform.position, Target.Transform.position) - CharacterData.AttackDistance;
				
				Debug.Log($"Target = {Target != null}");
				Debug.Log($"Target movers = {Target.Movers != null}");
				
				foreach (var mover in Target.Movers)
				{
					mover.LockMove();

					if (!mover.TryMoveToDirectionOnDistance(direction, distance, _timeToTarget, out Action move))
					{
						mover.UnlockMove();
					}
					
					// TODO 
				}
			}
		}

		private bool CanJerk()
		{
			if (Target.Damagable == null)
			{
				return false;
			}
			
			return CharacterData.AttackDistance < Vector3.Distance(transform.position, Target.Transform.position);
		}
	}
}