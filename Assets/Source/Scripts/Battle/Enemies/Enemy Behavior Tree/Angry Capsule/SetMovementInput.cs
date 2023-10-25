
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace ArenaHero.Fight.Enemies.BehaviorTree.Angry_Capsule
{
    public class SetMovementInput : Action
    {
        public SharedBotInput SelfBotInput;
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            SelfBotInput.Value.TargetPosition = TargetPosition.Value;
            return TaskStatus.Success;
        }
    }
}