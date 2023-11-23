
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
    public class SetMovePoint : Action
    {
        [SerializeField] private SharedBotInput _selfBotInput;
        [SerializeField] private SharedEnemy _enemy;

        public override TaskStatus OnUpdate()
        {
            _selfBotInput.Value.CallMove(_enemy.Value.Target.Transform.position);
            
            return TaskStatus.Success;
        }
    }
}