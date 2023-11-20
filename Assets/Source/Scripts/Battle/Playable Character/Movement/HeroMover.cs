using ArenaHero.Battle.Skills;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using System;
using System.Collections;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class HeroMover : MonoBehaviour
    {
        [SerializeField] private float _timeToTarget;
        [SerializeField] private Rigidbody _selfRigidbody;

        protected Coroutine MoveCoroutine;
        protected IMovementInputHandler InputHandler;

        private ITargetHandler _targetHandler;

        protected Target Target => _targetHandler.Target;
        
        protected Rigidbody SelfRigidbody => _selfRigidbody;

        private void Awake() =>
            _targetHandler = GetComponentInParent<ITargetHandler>();

        protected abstract void OnMove(float direction);

        protected void LookTarget()
        {
            var offset = Target.Transform.position - SelfRigidbody.position;
            offset.Set(offset.x, 0, offset.z);
            SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
        }

        protected IEnumerator Move(Func<bool> canMove, Func<float, Vector3> calculatePosition, Action endMoveCallBack = null)
        {
            float currentTime = 0;

            while (currentTime <= _timeToTarget)
            {
                if (!canMove())
                    break;

                SelfRigidbody.MovePosition(calculatePosition(currentTime / _timeToTarget));

                currentTime += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            endMoveCallBack?.Invoke();

            MoveCoroutine = null;
        }
    }
}