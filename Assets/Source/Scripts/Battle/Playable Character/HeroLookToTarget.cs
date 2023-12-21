using System;
using ArenaHero.Utils.Other;
using ArenaHero.Utils.StateMachine;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
	[RequireComponent(typeof(Rigidbody))]
	public class HeroLookToTarget : MonoBehaviour
	{
		private IStateChangeable _stateChangeable;
		private CoroutineHolder _coroutineHolder;
		
		[Inject]
		private void Inject(LookTargetPoint lookTargetPoint, IStateChangeable stateChangeable)
		{
			var selfRigidbody = GetComponent<Rigidbody>();
			
			_coroutineHolder = new CoroutineHolder(
				this,
				() => enabled,
				() =>
				{
					var offset = lookTargetPoint.transform.position - selfRigidbody.position;
					offset.Set(offset.x, 0, offset.z);
					selfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
				},
				new WaitForFixedUpdate());

			_stateChangeable = stateChangeable;
			
			_stateChangeable.StateChanged += OnStateChanged;

			_coroutineHolder.Start();
		}

		private void OnDisable()
		{
			_stateChangeable.StateChanged -= OnStateChanged;
		}

		private void OnStateChanged(Type stateType)
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