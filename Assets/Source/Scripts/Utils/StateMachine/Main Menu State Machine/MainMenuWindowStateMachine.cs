using System;
using System.Collections.Generic;
using ArenaHero.Utils.StateMachine;

namespace ArenaHero.Utils.StateMachine
{
	public class MainMenuWindowStateMachine : WindowStateMachine
	{
		public MainMenuWindowStateMachine(Func<Dictionary<Type, State<WindowStateMachine>>> getStates) : base(getStates) { }
	}
}