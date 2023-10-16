﻿using Game.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Game.Hero
{
    public class HeroHorizontalMover : HeroMover
    {
        [SerializeField] private float _angleTarget;

        [Inject]
        protected override void Inject(IInputHandler handler)
        {
            InputHandler = handler;
            InputHandler.Horizontal += OnMove;
        }

        public void ChangeCentralObject(GameObject newCentralObject)
        {
            Vector3 offset = transform.position - Target.transform.position;
            Target = newCentralObject;
            transform.position = Target.transform.position + offset;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                var targetTransform = Target.transform;
                var playerPosition = transform.position;

                var radius = Vector3.Distance(playerPosition, targetTransform.position);

                MoveCoroutine = StartCoroutine(Move((currentTime) =>
                {
                    Quaternion rotation = Quaternion.Euler(0f, _angleTarget * currentTime * -direction, 0f);
                    Vector3 newPosition = Target.transform.position + 
                                    (rotation * (transform.position - Target.transform.position).normalized * radius);

                    var offset = Target.transform.position - transform.position;
                    offset.Set(offset.x, 0, offset.z);
                    transform.rotation = Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f);

                    return newPosition;
                }));
            }
        }
    }
}