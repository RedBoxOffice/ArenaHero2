using System;
using ArenaHero.Battle.PlayableCharacter.Movement;
using ArenaHero.Utils.Other;
using ArenaHero.Utils.StateMachine;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
	public class LookTargetPointMover
	{
		private readonly CoroutineHolder _coroutineHolder;

		public LookTargetPointMover(MonoBehaviour context, float offsetDefaultPosition, Transform origin, Transform target)
		{
			Func<bool> actionCondition = () => true;

			if (target.TryGetComponent(out HeroHorizontalMover horizontalMover))
			{
				actionCondition = () => horizontalMover.IsMoving == false;
			}

			_coroutineHolder = new CoroutineHolder(
				context,
				() => context.enabled,
				() =>
				{
					if (origin.parent is null)
					{
						origin.position = target.position + (target.forward * offsetDefaultPosition);
					}
				},
				new WaitForFixedUpdate(),
				actionCondition);

			_coroutineHolder.Start();
		}

		public void OnStateChanged(Type stateType)
		{
			if (stateType == typeof(EndLevelState))
			{
				_coroutineHolder.Stop();
			}
			
			if (stateType == typeof(FightState))
			{
				_coroutineHolder.Reset();
			}
		}
	}
}