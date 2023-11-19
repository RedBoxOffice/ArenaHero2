using System;
using System.Collections.Generic;
using ArenaHero.UI;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.StateMachine.States;
using Reflex.Core;
using UnityEngine;

namespace ArenaHero
{
	public class MainMenuSceneInstaller : MonoBehaviour, IInstaller
	{
		[Header("Navigation Zone Windows Buttons")]
		[SerializeField] private EventTriggerButton _equipmentButton;
		[SerializeField] private EventTriggerButton _selectLevelButton;
		[SerializeField] private EventTriggerButton _talentsButton;
		[SerializeField] private EventTriggerButton _magazineButton;

		private MainMenuWindowStateMachine _windowStateMachine;
		
		public void InstallBindings(ContainerDescriptor descriptor)
		{
			_windowStateMachine = new MainMenuWindowStateMachine(() => new Dictionary<Type, State<WindowStateMachine>>()
			{
				[typeof(EquipmentWindowState)] = new EquipmentWindowState(),
				[typeof(SelectLevelWindowState)] = new SelectLevelWindowState(),
				[typeof(TalentsWindowState)] = new TalentsWindowState(),
				[typeof(MagazineWindowState)] = new MagazineWindowState(),
			});

			descriptor.AddInstance(_windowStateMachine);

			var transitionInitializer = new TransitionInitializer<WindowStateMachine>(_windowStateMachine);
			
			transitionInitializer.InitTransition<EquipmentWindowState>(_equipmentButton);
			transitionInitializer.InitTransition<SelectLevelWindowState>(_selectLevelButton);
			transitionInitializer.InitTransition<TalentsWindowState>(_talentsButton);
			transitionInitializer.InitTransition<MagazineWindowState>(_magazineButton);
		}	
	}
}