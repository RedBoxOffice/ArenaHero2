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

        [Inject]
        private void Inject(PlayerInput input)
        {
            _input = input;
            
            _input.Movement.Horizontal.performed += _ => Horizontal?.Invoke(GetHorizontal());
            _input.Movement.Vertical.performed += _ => Vertical?.Invoke(GetVertical());
            _input.Actions.ChangeTarget.performed += _ => ChangeTarget?.Invoke();
            _input.Actions.Attack.performed += _ => Attack?.Invoke();
        }
        
        private float GetHorizontal() => _input.Movement.Horizontal.ReadValue<float>();
        
        private float GetVertical() => _input.Movement.Vertical.ReadValue<float>();
    }
}
