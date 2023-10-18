using ArenaHero.Fight.Hero.EnemyDetection;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using Reflex.Core;
using UnityEngine;

namespace ArenaHero
{
    public class FightSceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerState<GameStateMachine>
    {
        [SerializeField] private TriggerZone _triggerZone;
        [SerializeField] private Transform _defaultLookPoint;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            TargetChanger enemyChanger = new TargetChanger(_triggerZone, _defaultLookPoint);
        }

        public void OnSceneLoaded<TState>(GameStateMachine machine) where TState : State<GameStateMachine> =>
            machine.EnterIn<TState>();
    }
}
