using ArenaHero.Utils.StateMachine;

namespace ArenaHero.Utils.TypedScenes
{
    public interface ISceneLoadHandlerOnStateAndArgument<TMachine, in TArgument> 
        where TMachine : StateMachine<TMachine>
    {
        public void OnSceneLoaded<TState>(TMachine machine, TArgument argument) 
            where TState : State<TMachine>;
    }
}