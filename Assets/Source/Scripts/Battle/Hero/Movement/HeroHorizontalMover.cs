using ArenaHero.InputSystem;
using ArenaHero.Utils.Other;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Fight.Player.Movement
{
    public class HeroHorizontalMover : HeroMover
    {
        [SerializeField] private FloatRange _distanceRange = new(1, 10);
        [SerializeField] private FloatRange _radiusRange = new(1, 25);
        [SerializeField] private AnimationCurve _angleCurve;

        [Inject]
        protected override void Inject(IMovementInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Horizontal += OnMove;
        }

        public void ChangeCentralObject(Transform newCentralObject)
        {
            Vector3 offset = SelfRigidbody.position - Target.position;
            Target = newCentralObject;
            SelfRigidbody.position = Target.position + offset;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                var targetTransform = Target;
                var playerPosition = SelfRigidbody.position;

                var radius = Vector3.Distance(playerPosition, targetTransform.position);

                var distance = GetMoveDistance(radius);

                float defaultY = transform.position.y;

                MoveCoroutine = StartCoroutine(Move((currentTime) =>
                {
                    float angle = (distance * 360) / (2 * Mathf.PI * radius);

                    Quaternion rotation = Quaternion.Euler(0f, angle * currentTime * -direction, 0f);
                    Vector3 newPosition = Target.position + 
                                    (rotation * (SelfRigidbody.position - Target.position).normalized * radius);

                    newPosition.y = defaultY;

                    var offset = Target.position - SelfRigidbody.position;
                    offset.Set(offset.x, 0, offset.z);
                    SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));

                    return newPosition;
                }));
            }
        }

        private float GetMoveDistance(float radius)
        {
            var deltaRadius = _radiusRange.Max - _radiusRange.Min;

            var normalRadius = Mathf.Clamp01(radius / deltaRadius);
            var normalDistance = _angleCurve.Evaluate(normalRadius);

            return Mathf.Clamp(_distanceRange.Min + normalDistance, _distanceRange.Min, _distanceRange.Max);
        }
    }
}