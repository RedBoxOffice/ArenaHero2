using System;
using System.Collections.Generic;
using ArenaHero.Game.UpgradeSystem;
using ArenaHero.UI;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.StateMachine.States;
using ArenaHero.Utils.TypedScenes;
using Reflex.Core;
using UnityEngine;

namespace ArenaHero
{
	public class MainMenuSceneInstaller : MonoBehaviour, IInstaller, ISceneLoadHandlerOnState<GameStateMachine, object>
    {
		[Header("Navigation Zone Windows Buttons")]
		[SerializeField] private EventTriggerButton _equipmentButton;
		[SerializeField] private EventTriggerButton _selectLevelButton;
		[SerializeField] private EventTriggerButton _talentsButton;
		[SerializeField] private EventTriggerButton _magazineButton;

		[Header("Upgrade")]
		[SerializeField] private CharacteristicUpdater _characteristicUpdater;

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

			descriptor.AddInstance(_characteristicUpdater, typeof(IModelHandler ));
		}

        public void OnSceneLoaded<TState>(GameStateMachine machine, object argument = default)
			where TState : State<GameStateMachine>
        {			
            GetComponent<WindowInitializer>().WindowsInit(machine.Window);

            machine.EnterIn<TState>();
        }
    }
}