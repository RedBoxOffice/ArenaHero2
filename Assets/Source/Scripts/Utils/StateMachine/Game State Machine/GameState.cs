namespace ArenaHero.Utils.StateMachine
{
    public abstract class GameState : State<GameStateMachine>
    {
        protected GameState(WindowStateMachine windowStateMachine) =>
            WindowStateMachine = windowStateMachine;
        
        protected WindowStateMachine WindowStateMachine { get; }
    }
}