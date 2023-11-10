using System;
using ArenaHero.Utils.StateMachine.States;

namespace ArenaHero.Utils.StateMachine.Windows
{
	public class SelectLevelWindow : Window
	{
		public override Type WindowType => typeof(SelectLevelWindowState);
	}
}