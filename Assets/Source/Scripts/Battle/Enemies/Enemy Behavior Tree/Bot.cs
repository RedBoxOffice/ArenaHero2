using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public class Bot : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour _inputSourceBehaviour;
        private IBotInputHandler _inputSource;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _inputSource = (IBotInputHandler)_inputSourceBehaviour;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var movement = _inputSource.TargetPosition;
            _agent.SetDestination(movement);
        }

        private void OnValidate()
        {
            if (_inputSourceBehaviour && _inputSourceBehaviour is not IBotInputHandler)
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IBotInputHandler));
                _inputSourceBehaviour = null;
            }
        }
    }
}