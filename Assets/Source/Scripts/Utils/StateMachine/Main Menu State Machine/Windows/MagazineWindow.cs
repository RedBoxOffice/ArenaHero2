using System;
using ArenaHero.Utils.StateMachine.States;

namespace ArenaHero.Utils.StateMachine.Windows
{
    public class MagazineWindow : Window
    {
        public override Type WindowType => typeof(MagazineWindowState);
    }
}

