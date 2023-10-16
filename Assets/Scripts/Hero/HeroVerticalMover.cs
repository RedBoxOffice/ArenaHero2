using Game.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Game.Hero
{
    public class HeroVerticalMover : HeroMover
    {
        [SerializeField] private float _distanceMove;

        [Inject]
        protected override void Inject(IInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Vertical += OnMove;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                Vector3 startPosition = transform.position;

                var targetPosition = transform.position + (direction * _distanceMove * transform.forward);

                MoveCoroutine = StartCoroutine(Move((currentTime) =>
                    Vector3.Lerp(startPosition, targetPosition, currentTime / TimeToTarget)));
            }
        }
    }
}