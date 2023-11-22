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

        public override bool TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget)
        {
            if (direction != Vector3.forward || direction != Vector3.back)
                return false;

            Move(direction, distance, timeToTarget);

            return true;
        }
        
        protected override void OnMove(float direction) =>
            Move(Vector3.forward * direction);

        private void Move(Vector3 direction, float distance = 0, float timeToTarget = 0)
        {
            if (MoveCoroutine != null || IsMoveLocked)
                return;

            var startPosition = SelfRigidbody.position;

            distance = distance == 0 ? _distanceMove : distance;

            var targetPosition = startPosition + (direction.z * distance * transform.forward);

            LookTarget();

            MoveCoroutine = StartCoroutine(Move(
                canMove:() => DistanceToTarget > 5 || direction == Vector3.back,
                calculatePosition: normalTime => Vector3.Lerp(startPosition, targetPosition, normalTime),
                endMoveCallBack: LookTarget,
                timeToTarget: timeToTarget));
        }
    }
}