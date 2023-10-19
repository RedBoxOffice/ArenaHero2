using System;

namespace ArenaHero.Utils.StateMachine
{
    public class OverState : GameState, IOverFight
    {
        public OverState(WindowStateMachine windowStateMachine) : base(windowStateMachine) { }

        public event Action Over;

        public override void Enter()
        {
            WindowStateMachine.EnterIn<OverWindowState>();
            Over?.Invoke();
        }
        public override void Exit() => throw new NotImplementedException();
    }
}
