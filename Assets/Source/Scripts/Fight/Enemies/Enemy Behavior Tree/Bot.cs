using UnityEngine;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    [RequireComponent(typeof(CharacterController))]
    public class Bot : MonoBehaviour
    {
        private const float Speed = 5f;

        [SerializeField]
        private MonoBehaviour _inputSourceBehaviour;
        private IBotInputhandler _inputSource;

        private CharacterController _characterController;

        private void Awake()
        {
            _inputSource = (IBotInputhandler)_inputSourceBehaviour;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
            movement *= Speed;
            _characterController.SimpleMove(movement);
        }

        private void OnValidate()
        {
            if (_inputSourceBehaviour && !(_inputSourceBehaviour is IBotInputhandler))
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IBotInputhandler));
                _inputSourceBehaviour = null;
            }
        }
    }
}