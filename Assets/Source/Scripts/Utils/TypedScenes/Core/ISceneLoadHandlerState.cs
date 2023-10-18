using ArenaHero.Utils.StateMachine;

namespace ArenaHero.Utils.TypedScenes
{
    public interface ISceneLoadHandlerState<TMachine> where TMachine : StateMachine<TMachine>
    {
        void OnSceneLoaded<TState>(TMachine machine) where TState : State<TMachine>;
    }

    public interface ISceneLoadHandler<T>
    {
        void OnSceneLoaded(T argument);
    }

    public interface ISceneLoadHandler
    {
        void OnSceneLoaded();
    }
}