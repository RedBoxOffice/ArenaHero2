using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Data;
using ArenaHero.Game.Level;
using ArenaHero.InputSystem;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using Cinemachine;
using Reflex.Core;
using UnityEngine;

namespace ArenaHero
{
    public class FightSceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerOnState<GameStateMachine, LevelData>
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private LookTargetPoint _lookTargetPoint;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

        private Hero _hero;
        private LevelData _levelData;
        private LevelInitializer _levelInitializer;
        
        private Hero Hero => GetHeroInitialized();

        private void OnDisable()
        {
            _levelInitializer.Dispose();
        }

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
            
            descriptor.AddInstance(_lookTargetPoint);
            descriptor.AddInstance(_levelData);
            descriptor.AddInstance(_waveHandler);
            descriptor.AddInstance(Hero);
        }

        public void OnSceneLoaded<TState>(GameStateMachine machine, LevelData argument = default)
            where TState : State<GameStateMachine>
        {
            _levelData = argument;
            GetComponent<UIFightSceneInitializer>().Init(machine);
            _levelInitializer = new LevelInitializer(_levelData, _waveHandler, new Target(Hero.transform, Hero.gameObject.GetComponent<IDamagable>()));
        }

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
