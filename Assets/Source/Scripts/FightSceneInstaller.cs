using ArenaHero.Data;
using ArenaHero.Fight.Player.EnemyDetection;
using ArenaHero.Fight.Level;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using Reflex.Core;
using UnityEngine;
using ArenaHero.Fight.Player;

namespace ArenaHero
{
    public class FightSceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerState<GameStateMachine>
    {
        [SerializeField] private TriggerZone _triggerZone;
        [SerializeField] private Transform _lookTargetPoint;
        [SerializeField] private LevelData _levelData;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Hero _hero;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            TargetChanger targetChanger = new TargetChanger(_triggerZone, _lookTargetPoint);
            descriptor.AddInstance(targetChanger, typeof(TargetChanger));

            descriptor.AddInstance(_levelData);
            descriptor.AddInstance(_waveHandler);
            descriptor.AddInstance(_hero);
        }

        public void OnSceneLoaded<TState>(GameStateMachine machine) where TState : State<GameStateMachine> =>
            machine.EnterIn<TState>();
    }
}
