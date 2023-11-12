using ArenaHero.Utils.StateMachine;

namespace ArenaHero.Utils.TypedScenes
{
    public interface ISceneLoadHandlerOnState<TMachine, in TArgument> where TMachine : StateMachine<TMachine>
    {
        void OnSceneLoaded<TState>(TMachine machine, TArgument argument = default) where TState : State<TMachine>;
    }
}