using System;

namespace ArenaHero.Utils.StateMachine
{
	public interface IStateChangeable
	{
		public event Action<Type> StateChanged;
	}
}