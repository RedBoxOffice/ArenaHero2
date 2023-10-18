using Base.StateMachine;
using Base.TypedScenes;
using Reflex.Core;
using UnityEngine;

namespace Game
{
    public class SceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerState<GameStateMachine>
    {
        public void InstallBindings(ContainerDescriptor descriptor)
        {

        }

        public void OnSceneLoaded<TState>(GameStateMachine machine) where TState : State<GameStateMachine> =>
            machine.EnterIn<TState>();
    }
}
