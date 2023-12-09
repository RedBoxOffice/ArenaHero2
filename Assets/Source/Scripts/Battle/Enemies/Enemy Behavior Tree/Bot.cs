using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
	public class Bot : MonoBehaviour, IMover
	{
		private IBotInputHandler _inputSource;
		private NavMeshAgent _agent;
		
		private void Awake()
		{
			_inputSource = GetComponent<IBotInputHandler>();
			_agent = GetComponent<NavMeshAgent>();
		}

		private void OnEnable() =>
			_inputSource.Move += OnMove;

		private void OnDisable() =>
			_inputSource.Move -= OnMove;

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