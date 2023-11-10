using System;

namespace ArenaHero.Utils.StateMachine
{
	public class FightWindow : Window
	{
		public override Type WindowType => typeof(FightWindowState);
	}
}