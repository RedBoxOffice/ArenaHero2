using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.InputSystem;
using ArenaHero.Utils.Other;
using Reflex.Attributes;
using System;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
	public class HeroHorizontalMover : HeroMover
	{
		[SerializeField] private HorizontalMove _radius;
		[SerializeField] private HorizontalMove _distance;

		[Inject]
		private void Inject(IMovementInputHandler handler)
		{
			InputHandler = handler;
			InputHandler.Horizontal += OnMove;
		}

		private void OnDisable() =>
			InputHandler.Horizontal -= OnMove;

		protected override void OnMove(float direction)
		{
			if (MoveCoroutine != null)
				return;

			var targetPosition = Target.Transform.position;

			var radius = Vector3.Distance(SelfRigidbody.position, targetPosition);

			var distance = _distance.DistanceRange.Min + _distance.GetNormalValue(radius) * _distance.DistanceDelta;

			var selfStartPosition = SelfRigidbody.position;

			var angle = (distance * 360) / (2 * Mathf.PI * radius);

			MoveCoroutine = StartCoroutine(Move(() => true,
				(normalTime) =>
				{
					var currentAngle = Mathf.Lerp(0f, angle, normalTime);
					var rotation = Quaternion.Euler(0f, currentAngle * -direction, 0f);

					radius *= _radius.GetNormalValue(radius);

					var newPosition = targetPosition +
						rotation * (selfStartPosition - targetPosition).normalized * radius;

					newPosition.y = selfStartPosition.y;

					LookTarget();

					return newPosition;
				}
			));
		}
	}
}