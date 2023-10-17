using Base.StateMachine;
using Base.TypedScenes;
using Reflex.Core;
using UnityEngine;

namespace Game
{
    public class SceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerState<GameStateMachine>
    {
        [SerializeField] private TriggerZone _triggerZone;
        [SerializeField] private Transform _defaultLookPoint;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            EnemyChanger enemyChanger = new EnemyChanger(_triggerZone, _defaultLookPoint);
        }

        public void OnSceneLoaded<TState>(GameStateMachine machine) where TState : State<GameStateMachine> =>
            machine.EnterIn<TState>();
    }
}
