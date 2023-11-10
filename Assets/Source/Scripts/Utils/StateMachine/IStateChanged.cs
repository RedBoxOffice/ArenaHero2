using System;

namespace ArenaHero.Utils.StateMachine
{
	public interface IStateChanged
	{
		public event Action StateChanged;
	}
}