namespace ArenaHero.Utils.StateMachine
{
	public class EndLevelState : GameState
	{
		public EndLevelState(WindowStateMachine windowStateMachine) : base(windowStateMachine)
		{
		}

		public override void Enter() =>
			WindowStateMachine.EnterIn<EndLevelWindowState>();

		public override void Exit()
		{
		}
	}
}