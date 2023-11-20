using ArenaHero.Utils.StateMachine.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine.Windows
{
    public class MagazineWindow : Window
    {
        public override Type WindowType => typeof(MagazineWindowState);
    }
}

