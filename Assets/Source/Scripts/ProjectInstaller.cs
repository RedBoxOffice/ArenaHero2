using Agava.YandexGames;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Yandex;
using ArenaHero.Yandex.Saves;
using Reflex.Core;
using System;
using System.Collections.Generic;
using ArenaHero.Debugs;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;

namespace ArenaHero
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader = new SceneLoader();

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            InputInit(descriptor);

            var context = GameContextInit();

            var gameStateMachine = GameStateMachineInit();

            descriptor.AddInstance(gameStateMachine, typeof(IStateChangeable));

            YandexInit(descriptor, context, gameStateMachine);

            descriptor.AddInstance(_sceneLoader);
        }

        private void InputInit(ContainerDescriptor descriptor)
        {
            var playerInput = new PlayerInput();
            playerInput.Enable();

            descriptor.AddInstance(playerInput);
        }

        private ApplicationFocusHandler GameContextInit()
        {
            var context = new GameObject(nameof(ApplicationFocusHandler)).AddComponent<ApplicationFocusHandler>();
            DontDestroyOnLoad(context);

            return context;
        }

        private GameStateMachine GameStateMachineInit()
        {
            var windowStateMachine = new WindowStateMachine(() => new Dictionary<Type, State<WindowStateMachine>>()
            {
                [typeof(FightWindowState)] = new FightWindowState(),
                [typeof(EndLevelWindowState)] = new EndLevelWindowState(),
                [typeof(MenuWindowState)] = new MenuWindowState(),
                [typeof(PauseWindowState)] = new PauseWindowState(),
            });

            var gameStateMachine = new GameStateMachine(windowStateMachine, () => new Dictionary<Type, State<GameStateMachine>>()
            {
                [typeof(FightState)] = new FightState(windowStateMachine),
                [typeof(EndLevelState)] = new EndLevelState(windowStateMachine),
                [typeof(MenuState)] = new MenuState(windowStateMachine),
                [typeof(PauseState)] = new PauseState(windowStateMachine)
            });

            return gameStateMachine;
        }

        private void YandexInit(ContainerDescriptor descriptor, ApplicationFocusHandler applicationFocusHandler, GameStateMachine gameStateMachine)
        {
            var saver = new GameDataSaver();

            var yandexSDKInitializer = new GameObject(nameof(YandexInitializer)).AddComponent<YandexInitializer>();
            yandexSDKInitializer.Init(gameStateMachine, _sceneLoader, () =>
            {
                saver.Init();

                string lang = "ru";
#if !UNITY_EDITOR
                lang = YandexGamesSdk.Environment.i18n.lang;
#endif
            });
        }
    }
}
