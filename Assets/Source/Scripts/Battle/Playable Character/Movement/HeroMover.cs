using ArenaHero.InputSystem;
using System;
using System.Collections;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class HeroMover : MonoBehaviour
    {
        [SerializeField] protected float TimeToTarget;
        [SerializeField] protected Transform Target;
        [SerializeField] protected Rigidbody SelfRigidbody;

        protected Coroutine MoveCoroutine;
        protected IMovementInputHandler InputHandler;

        protected abstract void OnMove(float direction);

        protected void LookTarget()
        {
            var offset = Target.position - SelfRigidbody.position;
            offset.Set(offset.x, 0, offset.z);
            SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
        }

        protected IEnumerator Move(Func<bool> canMove, Func<float, Vector3> calculatePosition, Action endMoveCallBack = null)
        {
            float currentTime = 0;

            while (currentTime <= TimeToTarget)
            {
                if (!canMove())
                    break;

                SelfRigidbody.MovePosition(calculatePosition(currentTime / TimeToTarget));

                currentTime += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            endMoveCallBack?.Invoke();

            MoveCoroutine = null;
        }
    }
}