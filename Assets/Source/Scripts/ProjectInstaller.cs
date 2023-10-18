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
using Unity.VisualScripting;
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
            var backgoundAudio = new GameObject(nameof(AudioSource)).AddComponent<AudioSource>();
            var gameAudio = backgoundAudio.gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(backgoundAudio);
            _gameAudioHandler.Init();
            var audioController = new AudioController(gameAudio, backgoundAudio, _gameAudioHandler, context);


            descriptor.AddInstance(audioController, typeof(IAudioController));
        }

        private GameStateMachine GameStateMachineInit()
        {
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
