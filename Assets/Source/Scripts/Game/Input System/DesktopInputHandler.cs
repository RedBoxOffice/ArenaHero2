using Reflex.Attributes;
using System;
using UnityEngine;

namespace ArenaHero.InputSystem
{
    public class DesktopInputHandler : MonoBehaviour, IInputHandler
    {
        private PlayerInput _input;

        public event Action<float> Horizontal;
        public event Action<float> Vertical;
        public event Action ChangeTarget;

        private void OnEnable()
        {
            if (_input != null)
            {
                _input.Desktop.Horizontal.performed += ctx => OnHorizontal();
                _input.Desktop.Vertical.performed += ctx => OnVertical();
                _input.Desktop.ChangeTarget.performed += ctx => OnChangeTarget();
            }
        }

        private void OnDisable()
        {
            if (_input != null)
            {
                _input.Desktop.Horizontal.performed -= ctx => OnHorizontal();
                _input.Desktop.Vertical.performed -= ctx => OnVertical();
                _input.Desktop.ChangeTarget.performed -= ctx => OnChangeTarget();
            }
        }

        private void OnHorizontal() =>
            Horizontal?.Invoke(GetHorizontal());

        private void OnVertical() =>
            Vertical?.Invoke(GetVertical());

        private void OnChangeTarget() =>
            ChangeTarget?.Invoke();

        private float GetHorizontal() => _input.Desktop.Horizontal.ReadValue<float>();
        private float GetVertical() => _input.Desktop.Vertical.ReadValue<float>();

        [Inject]
        private void Inject(PlayerInput handler)
        {
            _input = handler;
            _input.Desktop.Horizontal.performed += ctx => OnHorizontal();
            _input.Desktop.Vertical.performed += ctx => OnVertical();
            _input.Desktop.ChangeTarget.performed += ctx => OnChangeTarget();
        }
    }
}
