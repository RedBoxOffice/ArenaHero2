using System;

namespace ArenaHero.InputSystem
{
	public interface IActionsInputHandlerOnlyPlayer
	{
		public event Action ChangeTarget;

		public event Action Skill;
	}
}