namespace Base.StateMachine
{
    public abstract class GameState : State<GameStateMachine>
    {
        protected WindowStateMachine WindowStateMachine { get; private set; }

        public GameState(WindowStateMachine windowStateMachine) =>
            WindowStateMachine = windowStateMachine;
    }
}