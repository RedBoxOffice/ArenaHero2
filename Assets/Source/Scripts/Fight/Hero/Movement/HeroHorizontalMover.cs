using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Fight.Player.Movement
{
    public class HeroHorizontalMover : HeroMover
    {
        [SerializeField] private float _angleTarget;

        [Inject]
        protected override void Inject(IMovementInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Horizontal += OnMove;
        }

        public void ChangeCentralObject(GameObject newCentralObject)
        {
            Vector3 offset = SelfRigidbody.position - Target.transform.position;
            Target = newCentralObject;
            SelfRigidbody.position = Target.transform.position + offset;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                var targetTransform = Target.transform;
                var playerPosition = SelfRigidbody.position;

                var radius = Vector3.Distance(playerPosition, targetTransform.position);

                float defaultY = transform.position.y;

                MoveCoroutine = StartCoroutine(Move((currentTime) =>
                {
                    Quaternion rotation = Quaternion.Euler(0f, _angleTarget * currentTime * -direction, 0f);
                    Vector3 newPosition = Target.transform.position + 
                                    (rotation * (SelfRigidbody.position - Target.transform.position).normalized * radius);

                    newPosition.y = defaultY;

                    var offset = Target.transform.position - SelfRigidbody.position;
                    offset.Set(offset.x, 0, offset.z);
                    SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));

                    return newPosition;
                }));
            }
        }
    }
}