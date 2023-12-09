using System;
using System.Collections.Generic;

namespace ArenaHero.Utils.StateMachine
{
	public class MainMenuWindowStateMachine : WindowStateMachine
	{
		public MainMenuWindowStateMachine(Func<Dictionary<Type, State<WindowStateMachine>>> getStates) : base(getStates)
		{
		}
	}
}