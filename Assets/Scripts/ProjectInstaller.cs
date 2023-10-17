using Agava.YandexGames;
using Base.StateMachine;
using Base.Yandex;
using Base.Yandex.AD;
using Base.Yandex.Localization;
using Base.Yandex.Saves;
using Custom.States;
using Reflex.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        GameStateMachine gameStateMachine;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            var playerInput = new PlayerInput();
            playerInput.Enable();

            descriptor.AddInstance(playerInput);

            var context = new GameObject(nameof(Context)).AddComponent<Context>();
            DontDestroyOnLoad(context);

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
