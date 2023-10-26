using ArenaHero.InputSystem;
using System.Collections;
using UnityEngine;

namespace ArenaHero.Fight.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class HeroMover : MonoBehaviour
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

        protected abstract void Inject(IMovementInputHandler inputHandler);
        protected abstract void OnMove(float direction);

        protected void LookTarget()
        {
            var offset = Target.position - SelfRigidbody.position;
            offset.Set(offset.x, 0, offset.z);
            SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
        }

        protected IEnumerator Move(System.Func<float, Vector3> calculatePosition, System.Action endMoveCallBack = null)
        {
            float currentTime = 0;

            while (currentTime <= TimeToTarget)
            {
                SelfRigidbody.MovePosition(calculatePosition(currentTime));

                currentTime += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            endMoveCallBack?.Invoke();

            MoveCoroutine = null;
        }
    }
}