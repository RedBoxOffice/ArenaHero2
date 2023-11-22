using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	public class Bot : MonoBehaviour, IMover
	{
		[SerializeField] private MonoBehaviour _inputSourceBehaviour;
		[SerializeField] private float _delayUpdateDestination = 0.3f;

		private IBotInputHandler _inputSource;
		private NavMeshAgent _agent;
		private Coroutine _moveCoroutine;

		public bool IsMoveLocked { get; private set; }

		private void Awake()
		{
			_inputSource = (IBotInputHandler)_inputSourceBehaviour;
			_agent = GetComponent<NavMeshAgent>();
		}

		private void OnValidate()
		{
			if (_inputSourceBehaviour && _inputSourceBehaviour is not IBotInputHandler)
			{
				Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IBotInputHandler));
				_inputSourceBehaviour = null;
			}
		}

		public bool TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget)
		{
			if ()
		}

		public void LockMove() =>
			IsMoveLocked = true;

		public void UnlockMove() =>
			IsMoveLocked = false;

		public void OnMove()
		{
			if (_moveCoroutine != null || IsMoveLocked)
			{
				return;
			}
		}
		
		private IEnumerator UpdateDestination()
		{
			var delay = new WaitForSeconds(_delayUpdateDestination);

			while (enabled)
			{
				Vector3 moveTarget;
				if (IsMoveLocked)
				{
					moveTarget = 
					
				}
				else
				{
					moveTarget = _inputSource.TargetPosition;
				}
				
				_agent.SetDestination(moveTarget);

				yield return delay;
			}
		}

		private IEnumerator Wait(Coroutine routine, Action )
		{
			yield return routine;
		}
	}
}