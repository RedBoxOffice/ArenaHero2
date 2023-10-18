using System;
using UnityEngine;

namespace Base.StateMachine
{
    public abstract class Window : MonoBehaviour
    {
        public abstract Type WindowType { get; }
    }
}