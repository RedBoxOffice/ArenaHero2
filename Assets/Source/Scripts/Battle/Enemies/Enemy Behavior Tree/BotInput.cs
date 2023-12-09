using System;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public class BotInput : MonoBehaviour, IBotInputHandler
    {
        public event Action Attack;
        
        public event Action<Vector3> Move;

        public void CallAttack() =>
            Attack?.Invoke();

        public void CallMove(Vector3 position) =>
            Move?.Invoke(position);
    }
}