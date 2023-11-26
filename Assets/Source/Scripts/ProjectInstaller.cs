using Agava.YandexGames;
using ArenaHero.Game.AudioControl;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Yandex;
using ArenaHero.Yandex.AD;
using ArenaHero.Yandex.Localization;
using ArenaHero.Yandex.Saves;
using Reflex.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private GameAudioHandler _gameAudioHandler;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            InputInit(descriptor);

            var context = GameContextInit();

            AudioInit(descriptor, context);

            var gameStateMachine = GameStateMachineInit();

            descriptor.AddInstance(gameStateMachine.TryGetState<EndLevelState>(), typeof(IEndLevelStateChanged));

            YandexInit(descriptor, context, gameStateMachine);
        }

        private void InputInit(ContainerDescriptor descriptor)
        {
            var playerInput = new PlayerInput();
            playerInput.Enable();

            descriptor.AddInstance(playerInput);
        }

        private Context GameContextInit()
        {
            var context = new GameObject(nameof(Context)).AddComponent<Context>();
            DontDestroyOnLoad(context);

            return context;
        }

        private void AudioInit(ContainerDescriptor descriptor, Context context)
        {
            var backgroundAudio = new GameObject(nameof(AudioSource)).AddComponent<AudioSource>();
            var gameAudio = backgroundAudio.gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(backgroundAudio);
            _gameAudioHandler.Init();
            var audioController = new AudioController(gameAudio, backgroundAudio, _gameAudioHandler, context);


            descriptor.AddInstance(audioController, typeof(IAudioController));
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
            }); ;

            return gameStateMachine;
        }

        private void YandexInit(ContainerDescriptor descriptor, Context context, GameStateMachine gameStateMachine)
        {
            var saver = new GamePlayerDataSaver();
            var ad = new Ad(context, countOverBetweenShowsAd: 5);
            descriptor.AddInstance(ad, typeof(ICounterForShowAd));

            var yandexSDKInitializer = new GameObject(nameof(YandexInitializer)).AddComponent<YandexInitializer>();
            yandexSDKInitializer.Init(gameStateMachine, () =>
            {
                saver.Init();

                string lang = "ru";
#if !UNITY_EDITOR
                lang = YandexGamesSdk.Environment.i18n.lang;
#endif
                GameLanguage.Value = lang;
            });

            descriptor.AddInstance(saver, typeof(ISaver));
        }
    }
}
