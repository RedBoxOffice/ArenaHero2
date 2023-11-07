using ArenaHero.Utils.StateMachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.TypedScenes
{
    public class LoadingProcessor : MonoBehaviour
    {
        private static LoadingProcessor _instance;
        protected Action LoadingModelAction;

        public static LoadingProcessor Instance
        {
            get
            {
                if (_instance == null)
                    Initialize();

                return _instance;
            }
        }

        private static void Initialize()
        {
            _instance = new GameObject("LoadingProcessor").AddComponent<LoadingProcessor>();
            _instance.transform.SetParent(null);
            DontDestroyOnLoad(_instance);
        }

        public void ApplyLoadingModel()
        {
            LoadingModelAction?.Invoke();
            LoadingModelAction = null;
        }

        public void RegisterLoadingModel<TMachine, TState>(TMachine machine)
            where TMachine : StateMachine<TMachine>
            where TState : State<TMachine> =>
            LoadingModelAction += () =>
                CallSceneLoaded<ISceneLoadHandlerOnState<TMachine>>((handler) => handler.OnSceneLoaded<TState>(machine));

        public void RegisterLoadingModel<T>(T argument) =>
            LoadingModelAction += () =>
                CallSceneLoaded<ISceneLoadHandlerOnArgument<T>>((handler) => handler.OnSceneLoaded(argument));

        public void RegisterLoadingModel() =>
            LoadingModelAction += () =>
                CallSceneLoaded<ISceneLoadHandler>((handler) => handler.OnSceneLoaded());

        private void CallSceneLoaded<THandler>(Action<THandler> onSceneLoaded)
        {
            foreach (var rootObjects in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var handler in rootObjects.GetComponentsInChildren<THandler>())
                {
                    onSceneLoaded(handler);
                }
            }
        }
    }
}
