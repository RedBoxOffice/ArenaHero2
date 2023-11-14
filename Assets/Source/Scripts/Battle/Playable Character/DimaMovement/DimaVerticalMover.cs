using System;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.DimaMovement
{
	public class DimaVerticalMove : DimaHeroMover
	{
		private float _distanceMove;
		
		[Inject]
		protected override void Inject(IMovementInputHandler inputHandler)
		{
			InputHandler = inputHandler;
			InputHandler.Vertical += OnMove;
		}

		protected override void OnMove(float direction)
		{
			if (MoveCoroutine == null)
			{
				StartCoroutine(Move(() => true,
					(currentTime) =>
				{
					Vector3 targetPosition = new Vector3(0, 0, 1) * direction * currentTime / _distanceMove; 
					float pointOfAttack = Vector3.Distance(Target.position, transform.position);

					if ( pointOfAttack < 2 && direction > 0)                    
						return Vector3.zero;
                    
					return targetPosition;
				}));
			}
		}
		protected override void OnMove(float direction, Action callBack) =>
			throw new NotImplementedException();
	}
}