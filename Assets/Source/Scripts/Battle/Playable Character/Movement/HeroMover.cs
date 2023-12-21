using System;
using System.Collections;
using ArenaHero.InputSystem;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class HeroMover : MonoBehaviour, IMover
    {
        [SerializeField] private float _timeToTarget;
        [SerializeField] private Rigidbody _selfRigidbody;
        [SerializeField] private int _countAssistedAttempts = 5;
        
        protected Coroutine MoveCoroutine;
        protected IMovementInputHandler InputHandler;
        
        private ITargetHolder _targetHolder;
        private NavMeshWorld _navMeshWorld;
        private NavMeshQuery _navMeshQuery;
        
        protected Target Target => _targetHolder.Target;
        
        protected Rigidbody SelfRigidbody => _selfRigidbody;
        
        private void Awake() =>
            _targetHolder = GetComponentInParent<ITargetHolder>();
        
        protected virtual void OnDisable() =>
            _navMeshQuery.Dispose();

        private void OnEnable()
        {
            _navMeshWorld = NavMeshWorld.GetDefaultWorld();
            _navMeshQuery = new NavMeshQuery(_navMeshWorld, Allocator.None);
        }

        public abstract void TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget);
        
        protected abstract void OnMove(float direction);

        protected IEnumerator Move(Func<bool> canMove, Func<float, Vector3> calculatePosition, Action endMoveCallBack = null, float timeToTarget = 0)
        {
            float currentTime = 0;

            timeToTarget = timeToTarget == 0 ? _timeToTarget : timeToTarget;

            while (currentTime <= timeToTarget)
            {
                if (!canMove())
                {
                    break;
                }

                var targetPosition = GetTargetPosition(calculatePosition, currentTime, timeToTarget);
                
                bool isChecked = CheckPossibleMove(
                    targetPosition,
                    () => MoveAssistedAttempts(
                        time => GetTargetPosition(calculatePosition, time, timeToTarget), 
                        currentTime));
                
                if (isChecked is false)
                {
                    break;
                }
                
                SelfRigidbody.MovePosition(targetPosition);

                currentTime = AddTime(currentTime);
                
                yield return new WaitForFixedUpdate();
            }
    
            endMoveCallBack?.Invoke();

            MoveCoroutine = null;
        }

        protected void StopMove()
        {
            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
                MoveCoroutine = null;
            }
        }

        private float AddTime(float currentTime)
        {
            currentTime += Time.fixedDeltaTime;

            return currentTime;
        }

        private Vector3 GetTargetPosition(Func<float, Vector3> calculatePosition, float currentTime, float timeToTarget) =>
            calculatePosition(currentTime / timeToTarget);

        private bool CheckPossibleMove(Vector3 targetPosition, Func<bool> notCanMoveCallback)
        {
            if (CanMoveOnNavMesh(targetPosition) is false)
            {
                return notCanMoveCallback();
            }

            return true;
        }
        
        private bool MoveAssistedAttempts(Func<float, Vector3> getTargetPosition, float currentTime, int currentAttempt = 0)
        {
            currentTime = AddTime(currentTime);
            
            return CheckPossibleMove(
                getTargetPosition(currentTime),
                () =>
                {
                    if (currentAttempt >= _countAssistedAttempts)
                    {
                        return false;
                    }

                    return MoveAssistedAttempts(getTargetPosition, currentTime, ++currentAttempt);
                });
        }
        
        private bool CanMoveOnNavMesh(Vector3 targetPosition)
        {
            var navMeshPosition = _navMeshQuery.MapLocation(targetPosition, Vector3.positiveInfinity, 0).position;

            navMeshPosition.y = targetPosition.y;
            
            return navMeshPosition == targetPosition;
        }
    }
}