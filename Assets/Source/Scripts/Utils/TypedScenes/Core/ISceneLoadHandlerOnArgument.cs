namespace ArenaHero.Utils.TypedScenes
{
    public interface ISceneLoadHandlerOnArgument<T>
    {
        void OnSceneLoaded(T argument);
    }
}