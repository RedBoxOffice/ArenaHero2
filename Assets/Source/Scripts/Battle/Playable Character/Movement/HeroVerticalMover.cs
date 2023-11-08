using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    public class HeroVerticalMover : HeroMover
    {
        [SerializeField] private float _distanceMove;

        private float DistanceToTarget => Vector3.Distance(SelfRigidbody.position, Target.position);

        [Inject]
        private void Inject(IMovementInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Vertical += OnMove;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                Vector3 startPosition = SelfRigidbody.position;

                var distance = Mathf.Min(_distanceMove, DistanceToTarget);

                var targetPosition = SelfRigidbody.position + (direction * distance * transform.forward);

                LookTarget();

                MoveCoroutine = StartCoroutine(Move(
                    () => DistanceToTarget > 5 || direction == -1,
                    (currentTime) =>
                        Vector3.Lerp(startPosition, targetPosition, currentTime / TimeToTarget),
                    LookTarget));
            }
        }
    }
}