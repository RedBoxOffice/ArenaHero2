using ArenaHero.Utils.StateMachine;
using System;
using UnityEngine;

namespace ArenaHero.UI
{
    public abstract class EventTriggerButton : MonoBehaviour, ISubject
    {
        public event Action ActionEnded;

        public virtual void OnClick() =>
            ActionEnded?.Invoke();
    }
}