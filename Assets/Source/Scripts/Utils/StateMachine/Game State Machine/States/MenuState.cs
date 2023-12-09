namespace ArenaHero.Utils.StateMachine
{
	public class MenuState : GameState
	{
		public MenuState(WindowStateMachine windowStateMachine) : base(windowStateMachine)
		{
		}

		public override void Enter() =>
			WindowStateMachine.EnterIn<MenuWindowState>();

		public override void Exit()
		{
		}
	}
}