using Base.StateMachine;
using System;
using UnityEngine;

namespace Base.UI
{
    public abstract class EventTriggerButton : MonoBehaviour, ISubject
    {
        public bool IsInteractable = true;

        public virtual event Action Action;

        public virtual void OnClick()
        {
            if (!IsInteractable)
                return;

            Action?.Invoke();
        }
    }
}