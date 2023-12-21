using System;
using ArenaHero.Utils.StateMachine;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
	public class LookTargetPoint : MonoBehaviour
	{
		[SerializeField] [Range(2f, 20f)] private float _offsetDefaultPosition;

		private Hero _hero;
		private Action _unsubscribe;

		public Target Target { get; private set; }

		public void UpdateTarget(Target target) =>
			Target = target;

		[Inject]
		private void Inject(Hero hero, IStateChangeable stateChangeable)
		{
			_hero = hero;

			var lookTargetPointMover = new LookTargetPointMover(this, _offsetDefaultPosition, transform, _hero.transform);

			stateChangeable.StateChanged += lookTargetPointMover.OnStateChanged;

			_unsubscribe = () => stateChangeable.StateChanged -= lookTargetPointMover.OnStateChanged;
		}

		private void OnDisable() =>
			_unsubscribe?.Invoke();
	}
}