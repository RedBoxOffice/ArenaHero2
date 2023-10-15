using System;

namespace Game.Input
{
    public interface IInputHandler
    {
        public event Action<float> Horizontal;
        public event Action<float> Vertical;
    }
}
