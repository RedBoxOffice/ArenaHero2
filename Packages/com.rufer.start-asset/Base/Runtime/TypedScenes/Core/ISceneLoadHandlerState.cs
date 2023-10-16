using Base.StateMachine;

namespace Base.TypedScenes
{
    public interface ISceneLoadHandlerState<TMachine> where TMachine : StateMachine<TMachine>
    {
        void OnSceneLoaded<TState>() where TState : State<TMachine>;
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
