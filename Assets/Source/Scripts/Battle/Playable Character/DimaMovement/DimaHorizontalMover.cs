using System;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.DimaMovement
{
	public class DimaHorizontalMover : DimaHeroMover
	{
		private TargetChanger _targetChanger;
		private float _distanceMove;

		[Inject]
		private void Inject(IMovementInputHandler inputHandler, TargetChanger targetChanger)
		{
			base.Inject(inputHandler);

			_targetChanger = targetChanger;
			_targetChanger.TargetChanging += OnTargetChanging;
		}       

		protected override void OnMove(float direction)
		{
			if (MoveCoroutine == null)
			{
				StartCoroutine( Move(() => true,(currentTime) =>
				{
					Vector3 targetPosition = new Vector3(1, 0, 0) * direction * currentTime/_distanceMove; 
					return targetPosition;
				}));
			}
		}
		protected override void OnMove(float direction, Action callBack) =>
			throw new NotImplementedException();

		private void OnTargetChanging(Transform newCentralObject)
		{
			Target = newCentralObject;
		}
	}
}