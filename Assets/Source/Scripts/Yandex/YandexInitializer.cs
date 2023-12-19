using Agava.YandexGames;
using ArenaHero.Utils.StateMachine;
using System;
using System.Collections;
using ArenaHero.Debugs;
using UnityEngine;

namespace ArenaHero.Yandex
{
    public class YandexInitializer : MonoBehaviour
    {
        private Action _callBack;
        private GameStateMachine _gameStateMachine;

        private void Start() =>
            StartCoroutine(InitSDK());

        public void Init(GameStateMachine gameStateMachine, Action sdkInitSuccessCallBack)
        {
            _gameStateMachine = gameStateMachine;
            _callBack = sdkInitSuccessCallBack;
        }

        private IEnumerator InitSDK()
        {
#if !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize(_callBack);

            if (PlayerAccount.IsAuthorized == false)
                PlayerAccount.StartAuthorizationPolling(1500);
#else
            _callBack();
#endif

            SceneLoader.Instance.LoadMenu<MenuState, object>(_gameStateMachine);

            yield return null;
        }
    }
}
