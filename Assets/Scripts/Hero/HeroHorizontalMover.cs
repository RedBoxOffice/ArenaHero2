using Game.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Game.Hero
{
    public class HeroHorizontalMover : HeroMover
    {
        [SerializeField] private float _radius;

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                var targetTransform = Target.transform;
                var playerPosition = transform.position;

                var startAngleTarget = Target.transform.rotation.y;
                float angleCurrent = 0;

                var radius = Vector3.Distance(playerPosition, targetTransform.position);
                var angleTarget = (DistanceMove * 360) / (2 * Mathf.PI * radius);

                MoveCoroutine = StartCoroutine(Move((currentTime) =>
                {
                    angleCurrent += Mathf.Lerp(startAngleTarget, angleTarget, currentTime / TimeToTarget);
                    return (Quaternion.AngleAxis(angleCurrent * direction, Vector3.up) * new Vector3(radius, 0, 0)) + targetTransform.position;
                }));
            }
        }

        [Inject]
        protected override void Inject(IInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Horizontal += OnMove;
        }
    }
}