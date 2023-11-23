using System;

namespace ArenaHero.Utils.StateMachine
{
	public class EndLevelWindow : Window
	{
		public override Type WindowType => typeof(EndLevelWindowState);
	}
}