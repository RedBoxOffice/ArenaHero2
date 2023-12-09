using ArenaHero.Utils.TypedScenes;

namespace ArenaHero.Utils.StateMachine
{
	public sealed class SceneWindowInitializer : WindowInitializer, ISceneLoadHandlerOnState<GameStateMachine, object>
	{
		public void OnSceneLoaded<TState>(GameStateMachine machine, object argument = default) 
			where TState : State<GameStateMachine>
		{
			WindowsInit(machine.Window);

			machine.EnterIn<TState>();
		}		
	}
}