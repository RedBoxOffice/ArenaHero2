using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class JerkForAttackSkill : AttackSkill
	{
		protected override void OnAttack() =>
			TryJerk();

		private void TryJerk()
		{
			if (CanJerk())
			{
				Target.Damagable.TakeDamage(CharacterData.BaseDamage);
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