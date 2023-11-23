using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	public class Bot : MonoBehaviour, IMover
	{
		[SerializeField] private MonoBehaviour _inputSourceBehaviour;

		private IBotInputHandler _inputSource;
		private NavMeshAgent _agent;

		private void Awake()
		{
			_inputSource = (IBotInputHandler)_inputSourceBehaviour;
			_agent = GetComponent<NavMeshAgent>();
		}

		private void OnEnable() =>
			_inputSource.Move += OnMove;

		private void OnDisable() =>
			_inputSource.Move -= OnMove;

		private void OnValidate()
		{
			if (_inputSourceBehaviour && _inputSourceBehaviour is not IBotInputHandler)
			{
				Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IBotInputHandler));
				_inputSourceBehaviour = null;
			}
		}

		public void TryMoveToDirectionOnDistance(Vector3 direction, float distance, float timeToTarget)
		{
			Vector3 position = transform.position + (transform.forward * distance);
				
			UpdateDestination(position);
		}

		private void OnMove(Vector3 position) =>
			UpdateDestination(position);

		private void UpdateDestination(Vector3 position) =>
			_agent.SetDestination(position);
	}
}