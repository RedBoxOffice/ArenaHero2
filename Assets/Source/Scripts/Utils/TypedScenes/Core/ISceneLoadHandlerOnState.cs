using ArenaHero.Utils.StateMachine;

namespace ArenaHero.Utils.TypedScenes
{
    public interface ISceneLoadHandlerOnState<TMachine> where TMachine : StateMachine<TMachine>
    {
        void OnSceneLoaded<TState>(TMachine machine) where TState : State<TMachine>;
    }
}