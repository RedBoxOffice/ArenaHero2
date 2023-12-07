using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public class JerkForAttackSkill : AttackSkill
	{
		[SerializeField] private float _timeToTarget;

		private IMover[] _movers;
 
		protected override void Start()
		{
			base.Start();
			_movers = Character.GetComponents<IMover>();			
		}

		protected override void OnAttack() =>
			TryJerk();

		private void TryJerk()
		{
			if (CanJerk())
			{
				Vector3 direction = Vector3.forward;
				float distance = Vector3.Distance(transform.position, Target.Transform.position) - CharacterData.AttackDistance;
				
				foreach (var mover in _movers)
				{
					mover.TryMoveToDirectionOnDistance(direction, distance, _timeToTarget);
				}
			}
		}

		private bool CanJerk()
		{
			if (Target.Damageable == null)
			{
				return false;
			}
			
			return CharacterData.AttackDistance < Vector3.Distance(transform.position, Target.Transform.position);
		}
	}
}