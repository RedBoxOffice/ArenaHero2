using System.Collections;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
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
				
			Target.Damageable.TakeDamage(GameDataSaver.Instance.Get<Damage>().Value);
		}

		private bool CanAttack()
		{
			if (Target.Damageable == null || _cooldownCoroutine != null)
			{
				return false;
			}
			
			return !(CharacterData.AttackDistance < Vector3.Distance(transform.position, Target.Transform.position));
		}

		private IEnumerator Cooldown()
		{
			yield return new WaitForSeconds(CharacterData.AttackCooldown);

			_cooldownCoroutine = null;
		}
	}
}