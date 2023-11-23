using System;

namespace ArenaHero.Utils.StateMachine
{
    public class PauseWindow : Window
    {
        public override Type WindowType => typeof(PauseWindowState);
    }
}