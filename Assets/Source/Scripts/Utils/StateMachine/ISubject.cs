using System;

namespace ArenaHero.Utils.StateMachine
{
    public interface ISubject
    {
        public event Action ActionEnded;
    }
}