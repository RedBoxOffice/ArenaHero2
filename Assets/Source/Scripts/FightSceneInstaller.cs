using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Game.Level;
using ArenaHero.InputSystem;
using Cinemachine;
using Reflex.Core;
using UnityEngine;

namespace ArenaHero
{
	[RequireComponent(typeof(HeroInitializer))]
	[RequireComponent(typeof(LevelInitializer))]
	public class FightSceneInstaller : MonoBehaviour, IInstaller
	{
		[SerializeField] private CinemachineVirtualCamera _virtualCamera;

		public void InstallBindings(ContainerDescriptor descriptor)
		{
			var heroInitializer = GetComponent<HeroInitializer>();
			var hero = heroInitializer.Hero;

			var inputInstaller = GetComponent<InputHandlerInstaller>();
			var inputHandler = inputInstaller.InstallBindings(hero);
			descriptor.AddInstance(inputHandler, typeof(IMovementInputHandler), typeof(IActionsInputHandler));

			_virtualCamera.Follow = hero.gameObject.transform;

			var detectedZone = hero.gameObject.GetComponentInChildren<DetectedZone>();

			var targetChangerInject = new TargetChangerInject(() => (detectedZone, heroInitializer.LookTargetPoint, inputHandler));
			_ = new TargetChanger(targetChangerInject);

			var levelInitializer = GetComponent<LevelInitializer>();
			descriptor.AddInstance(levelInitializer.LevelData);
			descriptor.AddInstance(levelInitializer.RewardHandler);
		}
	}
}