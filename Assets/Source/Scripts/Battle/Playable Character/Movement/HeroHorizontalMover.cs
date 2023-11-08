﻿using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.InputSystem;
using ArenaHero.Utils.Other;
using Reflex.Attributes;
using System;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    public class HeroHorizontalMover : HeroMover, IDisposable
    {
        [SerializeField] private FloatRange _distanceRange = new(1, 10);
        [SerializeField] private FloatRange _radiusRange = new(1, 25);
        [SerializeField] private AnimationCurve _angleCurve;

        private TargetChanger _targetChanger;

        private float GetRadius => Vector3.Distance(SelfRigidbody.position, Target.position);

        public void Dispose()
        {
            if (_targetChanger != null)
                _targetChanger.TargetChanging -= OnTargetChanging;
        }

        [Inject]
        private void Inject(IMovementInputHandler handler, TargetChanger targetChanger)
        {
            InputHandler = handler;
            InputHandler.Horizontal += OnMove;

            _targetChanger = targetChanger;
            _targetChanger.TargetChanging += OnTargetChanging;
        }

        protected override void OnMove(float direction)
        {
            if (MoveCoroutine == null)
            {
                Transform targetTransform = Target;

                float distance = GetMoveDistance(GetRadius);

                float defaultY = transform.position.y;

                float angle = (distance * 360) / (2 * Mathf.PI * GetRadius);

                MoveCoroutine = StartCoroutine(Move(() => true,
                    (currentTime) =>
                    {
                        Quaternion rotation = Quaternion.Euler(0f, angle * currentTime * -direction, 0f);
                        Vector3 newPosition = Target.position + 
                                        (rotation * (SelfRigidbody.position - Target.position).normalized * GetRadius);

                        newPosition.y = defaultY;

                        LookTarget();

                        return newPosition;
                    }));
            }
        }

        private void OnTargetChanging(Transform newTarget)
        {
            //Vector3 offset = SelfRigidbody.position - newTarget.position;
            //Target = newTarget;
            //SelfRigidbody.position = Target.position + offset;
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