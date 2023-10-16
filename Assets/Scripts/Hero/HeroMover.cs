using Game.Input;
using System.Collections;
using UnityEngine;

namespace Game.Hero
{
    public abstract class HeroMover : MonoBehaviour
    {
        [SerializeField] protected float TimeToTarget;
        [SerializeField] protected GameObject Target;

        protected Coroutine MoveCoroutine;
        protected IInputHandler InputHandler;

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

        protected abstract void Inject(IInputHandler inputHandler);
        protected abstract void OnMove(float direction);

        protected IEnumerator Move(System.Func<float, Vector3> calculatePosition)
        {
            float currentTime = 0;

            while (currentTime <= TimeToTarget)
            {
                transform.position = calculatePosition(currentTime);

                currentTime += Time.deltaTime;

                yield return null;
            }

            MoveCoroutine = null;
        }
    }
}