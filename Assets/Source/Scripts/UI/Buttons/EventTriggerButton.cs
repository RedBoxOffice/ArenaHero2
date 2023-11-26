using ArenaHero.Utils.StateMachine;
using System;
using UnityEngine;

namespace ArenaHero.UI
{
    public abstract class EventTriggerButton : MonoBehaviour, ISubject
    {
        [SerializeField] private bool _isInteractable = true;
        
        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }

        public virtual event Action ActionEnded;

        public virtual void OnClick()
        {
            Debug.Log("Button Click");
            
            ActionEnded?.Invoke();
        }
    }
}