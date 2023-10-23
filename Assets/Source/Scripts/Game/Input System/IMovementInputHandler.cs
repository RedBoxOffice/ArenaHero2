using System;

namespace ArenaHero.InputSystem
{
    public interface IMovementInputHandler
    {
        public event Action<float> Horizontal;
        public event Action<float> Vertical;
    }
}
