using ArenaHero.Data;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Battle.Level;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using Reflex.Core;
using UnityEngine;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Game.Level;
using ArenaHero.InputSystem;
using Cinemachine;
using ArenaHero.Battle.Skills;

namespace ArenaHero
{
    [RequireComponent(typeof(FightSceneBeforeInitializer))]
    public class FightSceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerOnState<GameStateMachine, LevelData>
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private LookTargetPoint _lookTargetPoint;
        [SerializeField] private LevelData _levelData;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private DefaultAttackSkill _defaultAttackSkill;

        private Hero _hero;
        
        private Hero Hero => GetHeroInitialized();

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            var inputInstaller = GetComponent<InputHandlerInstaller>();
            
            _virtualCamera.Follow = Hero.gameObject.transform;

            var detectedZone = Hero.gameObject.GetComponentInChildren<DetectedZone>();
            
            var inputHandler = inputInstaller.InstallBindings(Hero);
            descriptor.AddInstance(inputHandler, typeof(IMovementInputHandler), typeof(IActionsInputHandler));

            var targetChangerInject = new TargetChangerInject(() => (detectedZone, _lookTargetPoint, inputHandler));
            var targetChanger = new TargetChanger(targetChangerInject);
            descriptor.AddInstance(targetChanger);

            descriptor.AddInstance(detectedZone);

            descriptor.AddInstance(_defaultAttackSkill);
            descriptor.AddInstance(_lookTargetPoint);
            descriptor.AddInstance(_levelData);
            descriptor.AddInstance(_waveHandler);
            descriptor.AddInstance(Hero);
        }
        
        public void OnSceneLoaded<TState>(GameStateMachine machine, LevelData argument = default)
            where TState : State<GameStateMachine> =>
            _ = new LevelInitializer(argument, _waveHandler, Hero);

        private Hero SpawnPlayer() =>
            Instantiate(_playerPrefab, _playerSpawnPoint.gameObject.transform.position, Quaternion.identity).GetComponentInChildren<Hero>();

        private Hero GetHeroInitialized()
        {
            if (_hero == null)
                _hero = SpawnPlayer().Init(_lookTargetPoint);

            return _hero;
        }
    }
}
