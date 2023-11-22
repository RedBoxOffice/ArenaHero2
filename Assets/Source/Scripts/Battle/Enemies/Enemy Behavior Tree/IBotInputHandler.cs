using System;
using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public interface IBotInputHandler : IActionsInputHandler
    {
        public event Action<Vector3> Move;
    }
}
