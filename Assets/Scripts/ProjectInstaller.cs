using Base.StateMachine;
using Game.States;
using Game.WindowStates;
using Reflex.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerDescriptor descriptor)
        {
            var playerInput = new PlayerInput();
            playerInput.Enable();

            descriptor.AddInstance(playerInput);

            var windowStateMachine = new WindowStateMachine(() =>
            {
                return new Dictionary<Type, State<WindowStateMachine>>()
                {
                    [typeof(FightWindowState)] = new FightWindowState()
                };
            });

            var gameStateMachine = new GameStateMachine(() =>
            {
                return new Dictionary<Type, State<GameStateMachine>>()
                {
                    [typeof(FightState)] = new FightState(windowStateMachine)
                };
            });

            Base.TypedScenes.Game.Load<FightState>(gameStateMachine);
        }
    }
}
