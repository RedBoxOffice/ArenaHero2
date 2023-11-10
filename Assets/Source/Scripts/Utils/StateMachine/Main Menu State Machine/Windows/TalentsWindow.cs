using System;
using ArenaHero.Utils.StateMachine.States;

namespace ArenaHero.Utils.StateMachine.Windows
{
	public class TalentsWindow : Window
	{
		public override Type WindowType => typeof(TalentsWindowState);
	}
}