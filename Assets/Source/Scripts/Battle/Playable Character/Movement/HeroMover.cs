﻿using System;
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
        
        protected Coroutine MoveCoroutine;
        protected IMovementInputHandler InputHandler;
        
        private ITargetHolder _targetHolder;
        private NavMeshWorld _navMeshWorld;
        private NavMeshQuery _navMeshQuery;
        
        protected Target Target => _targetHolder.Target;
        
        protected Rigidbody SelfRigidbody => _selfRigidbody;
        
        private void Awake() =>
            _targetHolder = GetComponentInParent<ITargetHolder>();

        private void Start()
        {
            _navMeshWorld = NavMeshWorld.GetDefaultWorld();
            _navMeshQuery = new NavMeshQuery(_navMeshWorld, Allocator.None);
        }

        protected virtual void OnDisable() =>
            _navMeshQuery.Dispose();

        public abstract void TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget);
        
        protected abstract void OnMove(float direction);

        protected void LookTarget()
        {
            var offset = Target.Transform.position - SelfRigidbody.position;
            offset.Set(offset.x, 0, offset.z);
            SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
        }

        protected IEnumerator Move(Func<bool> canMove, Func<float, Vector3> calculatePosition, Action endMoveCallBack = null, float timeToTarget = 0)
        {
            float currentTime = 0;

            timeToTarget = timeToTarget == 0 ? _timeToTarget : timeToTarget;

            while (currentTime <= timeToTarget)
            {
                if (!canMove())
                    break;

                Vector3 targetPosition = GetWorldPositionFromNavMesh(calculatePosition(currentTime / timeToTarget));
                
                SelfRigidbody.MovePosition(targetPosition);

                currentTime += Time.fixedDeltaTime;
                
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

        private Vector3 GetWorldPositionFromNavMesh(Vector3 targetPosition) =>
            _navMeshQuery.MapLocation(targetPosition, Vector3.positiveInfinity, 0).position;
    }
}