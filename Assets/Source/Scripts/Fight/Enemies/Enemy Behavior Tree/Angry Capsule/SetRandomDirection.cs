using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Fight.Enemies.BehaviorTree.AngryCapsule
{
    public class SetRandomDirection : Action
    {
        public SharedVector2 Direction;

        public override TaskStatus OnUpdate()
        {
            Direction.Value = Random.insideUnitCircle.normalized;
            return TaskStatus.Success;
        }
    }
}