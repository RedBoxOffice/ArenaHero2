using ArenaHero.Utils.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.TypedScenes
{
    public abstract class TypedScene<TMachine> where TMachine : StateMachine<TMachine>
    {
        protected static AsyncOperation LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }

        protected static AsyncOperation LoadScene<TState>(string sceneName, LoadSceneMode loadSceneMode, TMachine machine)
            where TState : State<TMachine>
        {
            LoadingProcessor.Instance.RegisterLoadingModel<TMachine, TState>(machine);
            return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }

        protected static AsyncOperation LoadScene<T>(string sceneName, LoadSceneMode loadSceneMode, T argument)
        {
            LoadingProcessor.Instance.RegisterLoadingModel(argument);
            return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }
    }

    public abstract class TypedScene
    {
        protected static AsyncOperation LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }

        protected static AsyncOperation LoadScene<T>(string sceneName, LoadSceneMode loadSceneMode, T argument)
        {
            LoadingProcessor.Instance.RegisterLoadingModel(argument);
            return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }
    }
}
