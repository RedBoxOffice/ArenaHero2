using System;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public class BotInput : MonoBehaviour, IBotInputHandler
    {
        public Vector3 TargetPosition { get; set; }
        
        public event Action Attack;

        public void CallAttack() =>
            Attack?.Invoke();
    }
}