using BehaviorDesigner.Runtime.Tasks;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
	public class SetAttack : Action
	{
		public SharedBotInput _botInput;

		public override TaskStatus OnUpdate()
		{
			_botInput.Value.CallAttack();
			
			return TaskStatus.Success;
		}
	}
}