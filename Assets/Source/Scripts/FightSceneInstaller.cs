using ArenaHero.Data;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Battle.Level;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using Reflex.Core;
using UnityEngine;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.InputSystem;

namespace ArenaHero
{
    public class FightSceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private DetectedZone _detectedZone;
        [SerializeField] private LookTargetPoint _lookTargetPoint;
        [SerializeField] private LevelData _levelData;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Hero _hero;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            var inputInstaller = GetComponent<InputHandlerInstaller>();
            var inputHandler = inputInstaller.InstallBindings(_hero);
            descriptor.AddInstance(inputHandler, typeof(IMovementInputHandler), typeof(IActionsInputHandler));

            var targetChangerInject = new TargetChangerInject(() => (_detectedZone, _lookTargetPoint, inputHandler));
            TargetChanger targetChanger = new TargetChanger(targetChangerInject);
            descriptor.AddInstance(targetChanger);

            descriptor.AddInstance(_detectedZone);

            descriptor.AddInstance(_levelData);
            descriptor.AddInstance(_waveHandler);
            descriptor.AddInstance(_hero);
        }
    }
}
