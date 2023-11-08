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
        public event Action Attack;

        //private void OnEnable()
        //{
        //    if (_input != null)
        //    {
        //        _input.Movement.Horizontal.performed += ctx => OnHorizontal();
        //        _input.Movement.Vertical.performed += ctx => OnVertical();
        //        _input.Actions.ChangeTarget.performed += ctx => OnChangeTarget();
        //        _input.Actions.Attack.performed += ctx => OnAttack();
        //    }
        //}

        private void OnDisable()
        {
            if (_input != null)
            {
                _input.Movement.Horizontal.performed -= ctx => OnHorizontal();
                _input.Movement.Vertical.performed -= ctx => OnVertical();
                _input.Actions.ChangeTarget.performed -= ctx => OnChangeTarget();
                _input.Actions.Attack.performed -= ctx => OnAttack();
            }
        }

        private void OnHorizontal() =>
            Horizontal?.Invoke(GetHorizontal());

        private void OnVertical() =>
            Vertical?.Invoke(GetVertical());

        private void OnChangeTarget() =>
            ChangeTarget?.Invoke();

        private void OnAttack() =>
            Attack?.Invoke();

        private float GetHorizontal() => _input.Movement.Horizontal.ReadValue<float>();
        private float GetVertical() => _input.Movement.Vertical.ReadValue<float>();

        [Inject]
        private void Inject(PlayerInput handler)
        {
            _input = handler;
            _input.Movement.Horizontal.performed += ctx => OnHorizontal();
            _input.Movement.Vertical.performed += ctx => OnVertical();
            _input.Actions.ChangeTarget.performed += ctx => OnChangeTarget();
            _input.Actions.Attack.performed += ctx => OnAttack();
        }
    }
}
