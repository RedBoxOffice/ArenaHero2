using ArenaHero.Utils.TypedScenes;

namespace ArenaHero.Utils.StateMachine
{
	public sealed class SceneWindowInitializer : WindowInitializer, ISceneLoadHandlerOnState<GameStateMachine>
	{
		public void OnSceneLoaded<TState>(GameStateMachine machine) where TState : State<GameStateMachine>
		{
			WindowsInit(machine.Window);

			machine.EnterIn<TState>();
		}
	}
}