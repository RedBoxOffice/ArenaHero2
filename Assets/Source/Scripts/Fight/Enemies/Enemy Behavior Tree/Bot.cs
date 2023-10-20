using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    public class Bot : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour _inputSourceBehaviour;
        private IBotInputhandler _inputSource;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _inputSource = (IBotInputhandler)_inputSourceBehaviour;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var movement = _inputSource.TargetPosition;
            _agent.SetDestination(movement);
        }

        private void OnValidate()
        {
            if (_inputSourceBehaviour && _inputSourceBehaviour is not IBotInputhandler)
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IBotInputhandler));
                _inputSourceBehaviour = null;
            }
        }
    }
}