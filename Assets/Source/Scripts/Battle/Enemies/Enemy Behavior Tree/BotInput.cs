using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public class BotInput : MonoBehaviour, IBotInputHandler
    {
        public Vector3 TargetPosition { get; set; }
        
        public event Action Attack;
        public event Action<Vector3> Move;

        public void CallAttack() =>
            Attack?.Invoke();

        public void CallMove(Vector3 position) =>
            Move?.Invoke(position);

        private void Start()
        {
            float delay = 1f;
            
            Invoke(nameof(Method), delay);

            StartCoroutine(DelayedCall(Method, delay));
        }

        private void Method()
        {
            Debug.Log($"INVOKEEEE");
        }
        
        private IEnumerator DelayedCall(Action method, float delay)
        {
            var delayTime = new WaitForSeconds(delay);

            yield return delayTime;

            method();
        }
    }
}