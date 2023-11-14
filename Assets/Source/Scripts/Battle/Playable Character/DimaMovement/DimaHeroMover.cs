using System;
using System.Collections;
using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.DimaMovement
{
	public abstract class DimaHeroMover : MonoBehaviour
    {
        [SerializeField] protected float TimeToTarget;
        [SerializeField] protected Transform Target;

        protected Coroutine MoveCoroutine;
        protected IMovementInputHandler InputHandler;
        protected Rigidbody SelfRigidbody; 

        private void Awake()
        {
            SelfRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (InputHandler != null)
            {
                InputHandler.Vertical += OnMove;
            }
        }

        private void OnDisable()
        {
            if (InputHandler != null)
            {
                InputHandler.Vertical -= OnMove;
            }
        }

        protected virtual void Inject(IMovementInputHandler inputHandler)
        {
            InputHandler = inputHandler;
            InputHandler.Horizontal += OnMove;
        }

        protected virtual void OnMove(float direction) =>
            OnMove(direction, null);

        protected abstract void OnMove(float direction, Action callBack);        

        protected void LookTarget()
        {
            var offset = Target.position - SelfRigidbody.position;
            offset.Set(offset.x, 0, offset.z);
            SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
        }

        protected IEnumerator Move(Func <bool> canMove,Func<float, Vector3> calculatePosition, Action endMoveCallBack = null, Action callBack = null)
        {
            float currentTime = 0;

            while (currentTime <= TimeToTarget)
            {
                if (canMove())
                {                   
                    callBack?.Invoke();
                    break;
                }

                SelfRigidbody.MovePosition(calculatePosition(currentTime));

                currentTime += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            endMoveCallBack?.Invoke();          

            MoveCoroutine = null;
        }
    }
}