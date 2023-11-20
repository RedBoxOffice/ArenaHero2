using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
    public class SetTargetPosition : Action
    {
        public SharedEnemy Enemy;
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            TargetPosition.Value = Enemy.Value.Target.Transform.position;
            return TaskStatus.Success;
        }
    }
}