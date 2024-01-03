using System.Collections;
using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class DefaultAttackSkill : AttackSkill
	{
		private Coroutine _cooldownCoroutine;
		
		protected override void OnAttack() =>
			TryAttackEnemy();
		
		private void TryAttackEnemy()
		{
			if (!CanAttack())
			{
				return;
			}
			
			_cooldownCoroutine = StartCoroutine(Cooldown());
				
			TargetHolder.Target.Damageable.TakeDamage(FeatureHolder.Get<DamageFeature>());
		}

		private bool CanAttack()
		{
			if (TargetHolder.Target.Damageable == null || _cooldownCoroutine != null)
			{
				return false;
			}
			
			return !(AttackDistance < Vector3.Distance(transform.position, TargetHolder.Target.Transform.position));
		}

		private IEnumerator Cooldown()
		{
			yield return new WaitForSeconds(AttackCooldown);

			_cooldownCoroutine = null;
		}
	}
}