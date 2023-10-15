using Reflex.Attributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class DesktopInputHandler : MonoBehaviour, IInputHandler
    {
        private PlayerInput _input;

        public event Action<float> Horizontal;
        public event Action<float> Vertical;

        private void OnEnable()
        {
            if (_input != null)
            {
                _input.Decktop.Horizontal.performed += ctx => OnHorizontal();
                _input.Decktop.Vertical.performed += ctx => OnVertical();
            }
        }

        private void OnDisable()
        {
            if (_input != null)
            {
                _input.Decktop.Horizontal.performed -= dsgwer => OnHorizontal();
                _input.Decktop.Vertical.performed -= ctx => OnVertical();
            }
        }

        private void OnHorizontal() =>
            Horizontal?.Invoke(GetHorizontal());

        private void OnVertical() =>
            Vertical?.Invoke(GetVertical());

        private float GetHorizontal() => _input.Decktop.Horizontal.ReadValue<float>();
        private float GetVertical() => _input.Decktop.Vertical.ReadValue<float>();

        [Inject]
        private void Inject(PlayerInput handler)
        {
            _input = handler;
            _input.Decktop.Horizontal.performed += ctx => OnHorizontal();
            _input.Decktop.Vertical.performed += ctx => OnVertical();
        }
    }
}
