using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public interface IBotInputHandler : IActionsInputHandler
    {
        public Vector3 TargetPosition { get; set; }
    }
}
