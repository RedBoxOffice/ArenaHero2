using Base.StateMachine;
using Game.WindowStates;
using System;

namespace Game.States
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