using ArenaHero.Battle.Skills;
using BehaviorDesigner.Runtime;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	public class SharedAttackSkill : SharedVariable<AttackSkill>
	{
		public static implicit operator SharedAttackSkill(AttackSkill input) =>
			new SharedAttackSkill
			{
				Value = input
			};
	}
}