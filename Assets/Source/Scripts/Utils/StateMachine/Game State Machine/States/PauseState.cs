namespace ArenaHero.Utils.StateMachine
{
    public class PauseState : GameState
    {
        public PauseState(WindowStateMachine windowStateMachine) : base(windowStateMachine)
        {
        }

        public override void Enter() =>
            WindowStateMachine.EnterIn<PauseWindowState>();

        public override void Exit()
        {
        }
    }
}

