using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Game.Level;
using ArenaHero.InputSystem;
using Cinemachine;
using Reflex.Core;
using UnityEngine;

namespace ArenaHero
{
	[RequireComponent(typeof(PlayerInitializer))]
	[RequireComponent(typeof(LevelInitializer))]
	public class FightSceneInstaller : MonoBehaviour, IInstaller
	{
		[SerializeField] private CinemachineVirtualCamera _virtualCamera;

		public void InstallBindings(ContainerDescriptor descriptor)
		{
			var playerInitializer = GetComponent<PlayerInitializer>();
			descriptor.AddInstance(playerInitializer.LookTargetPoint);
			var hero = playerInitializer.GetHero();
			descriptor.AddInstance(hero);
			
			var inputInstaller = GetComponent<InputHandlerInstaller>();
			var inputHandler = inputInstaller.InstallBindings(hero);
			descriptor.AddInstance(inputHandler, typeof(IMovementInputHandler), typeof(IActionsInputHandler));

			_virtualCamera.Follow = hero.gameObject.transform;

			var detectedZone = hero.gameObject.GetComponentInChildren<DetectedZone>();

			_ = new TargetChanger(detectedZone, playerInitializer.LookTargetPoint, inputHandler);

			var levelInitializer = GetComponent<LevelInitializer>();
			
			descriptor.AddInstance(levelInitializer.LevelData);
			descriptor.AddInstance(levelInitializer.RewardHandler);
		}
	}
}