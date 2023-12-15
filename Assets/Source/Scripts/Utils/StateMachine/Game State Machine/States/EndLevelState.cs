using System;

namespace ArenaHero.Utils.StateMachine
{
    public class EndLevelState : GameState, IEndLevelStateChanged
    {
        public EndLevelState(WindowStateMachine windowStateMachine) : base(windowStateMachine) =>
            Instance ??= this;

        public static IEndLevelStateChanged Instance { get; private set; }

        public event Action StateChanged;

        public override void Enter()
        {
            WindowStateMachine.EnterIn<EndLevelWindowState>();
            StateChanged?.Invoke();
        }

        public override void Exit()
        {
        }
    }
}
