using System;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    public class HeroVerticalMover : HeroMover
    {
        [SerializeField] private float _distanceMove;

        private float DistanceToTarget => Vector3.Distance(SelfRigidbody.position, Target.Transform.position);

        [Inject]
        private void Inject(IMovementInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Vertical += OnMove;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputHandler.Vertical -= OnMove;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine != null)
                return;
            
            var startPosition = SelfRigidbody.position;

            var targetPosition = startPosition + (direction * _distanceMove * transform.forward);

            LookTarget();

            MoveCoroutine = StartCoroutine(Move(
                () => DistanceToTarget > 5 || direction == (int)Direction.Back,
                (normalTime) =>
                    Vector3.Lerp(startPosition, targetPosition, normalTime),
                LookTarget));
        }
    }
}