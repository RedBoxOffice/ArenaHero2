using System;

namespace ArenaHero.InputSystem
{
    public interface IInputHandler
    {
        public event Action<float> Horizontal;
        public event Action<float> Vertical;
    }
}
