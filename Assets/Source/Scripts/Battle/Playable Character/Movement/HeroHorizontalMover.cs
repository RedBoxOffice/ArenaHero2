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

		protected override void OnDisable()
		{
			base.OnDisable();
			InputHandler.Horizontal -= OnMove;
		}

		protected override void OnMove(float direction) =>
			Move(Vector3.right * direction);

		public override bool TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget, out Action move)
		{
			move = null;
			
			if (direction != Vector3.left || direction != Vector3.right)
				return false;

			move = () => Move(direction, distance, timeToTarget);

			return true;
		}

		private void Move(Vector3 direction, float distance = 0, float timeToTarget = 0)
		{
			if (MoveCoroutine != null || IsMoveLocked)
				return;

			var targetPosition = Target.Transform.position;

			var radius = Vector3.Distance(SelfRigidbody.position, targetPosition);

			if (distance == 0)
			{
				distance = _distance.DistanceRange.Min + _distance.GetNormalValue(radius) * _distance.DistanceDelta;
			}

			var selfStartPosition = SelfRigidbody.position;

			var angle = (distance * 360) / (2 * Mathf.PI * radius);

			MoveCoroutine = StartCoroutine(Move(
				canMove: () => true,
				calculatePosition: normalTime =>
				{
					var currentAngle = Mathf.Lerp(0f, angle, normalTime);
					var rotation = Quaternion.Euler(0f, currentAngle * -direction.x, 0f);

					radius *= _radius.GetNormalValue(radius);

					var newPosition = targetPosition +
						rotation * (selfStartPosition - targetPosition).normalized * radius;

					newPosition.y = selfStartPosition.y;

					LookTarget();

					return newPosition;
				},
				timeToTarget: timeToTarget
			));
		}
	}
}