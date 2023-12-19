namespace ArenaHero.Utils.TypedScenes
{
	public interface ISceneLoadHandler
	{
		public void OnSceneLoaded<T>(T argument);
	}
}