using Base.StateMachine;

namespace Custom.States
{
    public class FightState : GameState
    {
        public FightState(WindowStateMachine windowStateMachine) : base(windowStateMachine) { }

        public override void Enter()
        {
            WindowStateMachine.EnterIn<FightWindowState>();
        }

        public override void Exit()
        {

        }
    }
}