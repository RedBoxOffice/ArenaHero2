using Game.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Game.Hero
{
    public class HeroHorizontalMover : HeroMover
    {
        [SerializeField] private float _angleTarget;

        private float _angleCurrent = 0;

        [Inject]
        protected override void Inject(IInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Horizontal += OnMove;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                var targetTransform = Target.transform;
                var playerPosition = transform.position;

                var startAngleTarget = Vector3.Angle(Target.transform.position, playerPosition);

                var radius = Vector3.Distance(playerPosition, targetTransform.position);

                MoveCoroutine = StartCoroutine(Move((currentTime) =>
                {
                    _angleCurrent += Mathf.Lerp(0, _angleTarget, currentTime / TimeToTarget) * direction;

                    float x = targetTransform.position.x + Mathf.Cos(_angleCurrent * Mathf.Deg2Rad) * radius;
                    float y = targetTransform.position.y;
                    float z = targetTransform.position.z + Mathf.Sin(_angleCurrent * Mathf.Deg2Rad) * radius;

                    return new Vector3()
                    {
                        x = x,
                        y = y,
                        z = z
                    };
                }));
            }
        }
    }
}