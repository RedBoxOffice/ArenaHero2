using System;

namespace ArenaHero.Utils.StateMachine
{
    public class EndLevelState : GameState, IEndLevelStateChanged
    {
        public EndLevelState(WindowStateMachine windowStateMachine) : base(windowStateMachine) { }

        public event Action StateChanged;

        public override void Enter()
        {
            WindowStateMachine.EnterIn<OverWindowState>();
            StateChanged?.Invoke();
        }
        public override void Exit() { }
    }
}
