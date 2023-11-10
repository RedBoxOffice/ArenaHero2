using System;

namespace ArenaHero.Utils.StateMachine
{
    public class MenuWindow : Window
    {
        public override Type WindowType => typeof(MenuWindowState);
    }
}