using System;

namespace ArenaHero.InputSystem
{
    public interface IActionsInputHandler
    {
        public event Action ChangeTarget;
        public event Action Attack;
    }
}
