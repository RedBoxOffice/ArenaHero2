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
			descriptor.AddInstance(hero);

			var inputInstaller = GetComponent<InputHandlerInstaller>();
			var inputHandler = inputInstaller.InstallBindings(hero);
			descriptor.AddInstance(inputHandler, typeof(IMovementInputHandler), typeof(IActionsInputHandler));

			_virtualCamera.Follow = hero.gameObject.transform;

			var detectedZone = hero.gameObject.GetComponentInChildren<DetectedZone>();
			descriptor.AddInstance(detectedZone);

			var targetChangerInject = new TargetChangerInject(() => (detectedZone, heroInitializer.LookTargetPoint, inputHandler));
			var targetChanger = new TargetChanger(targetChangerInject);
			descriptor.AddInstance(targetChanger);

			descriptor.AddInstance(heroInitializer.LookTargetPoint);

			var levelInitializer = GetComponent<LevelInitializer>();
			descriptor.AddInstance(levelInitializer.LevelData);
			descriptor.AddInstance(levelInitializer.WaveHandler);
			descriptor.AddInstance(levelInitializer.RewardHandler);
		}
	}
}