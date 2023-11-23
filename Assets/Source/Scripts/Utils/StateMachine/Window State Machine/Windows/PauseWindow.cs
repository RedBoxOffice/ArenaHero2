using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
    public class PauseWindow : Window
    {
        public override Type WindowType => typeof(PauseWindowState);
    }
}

