using System;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
    public abstract class Window : MonoBehaviour
    {
        public abstract Type WindowType { get; }
    }
}