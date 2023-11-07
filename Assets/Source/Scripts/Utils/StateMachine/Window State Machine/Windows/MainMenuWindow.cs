using System;

namespace ArenaHero.Utils.StateMachine
{
    public class MainMenuWindow : Window
    {
        public override Type WindowType => typeof(FightWindowState);
    }
}