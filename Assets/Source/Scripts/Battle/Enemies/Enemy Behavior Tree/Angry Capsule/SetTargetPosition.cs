using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
    public class SetTargetPosition : Action
    {
        public SharedAngryCapsule AngryCapsule;
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            TargetPosition.Value = AngryCapsule.Value.Target.position;
            return TaskStatus.Success;
        }
    }
}