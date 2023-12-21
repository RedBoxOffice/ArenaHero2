using ArenaHero.Utils.Other;
using ArenaHero.Utils.StateMachine;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
	[RequireComponent(typeof(Rigidbody))]
	public class HeroLookToTarget : MonoBehaviour
	{
		[Inject]
		private void Inject(LookTargetPoint lookTargetPoint, IStateChangeable stateChangeable)
		{
			var selfRigidbody = GetComponent<Rigidbody>();
			
			var coroutineHolder = new CoroutineHolder(
				this,
				() => enabled,
				() =>
				{
					var offset = lookTargetPoint.transform.position - selfRigidbody.position;
					offset.Set(offset.x, 0, offset.z);
					selfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
				},
				new WaitForFixedUpdate());

			stateChangeable.StateChanged += (stateType) =>
			{
				if (stateType == typeof(EndLevelState))
				{
					coroutineHolder.Stop();
				}
				
				if (stateType == typeof(FightState))
				{
					coroutineHolder.Reset();
				}
			};

			coroutineHolder.Start();
		}
	}
}