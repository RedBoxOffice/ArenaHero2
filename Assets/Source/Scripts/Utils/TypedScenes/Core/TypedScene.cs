using ArenaHero.Utils.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.TypedScenes
{
    public abstract class TypedScene<TMachine> where TMachine : StateMachine<TMachine>
    {
        protected static AsyncOperation LoadScene<TState, T>(string sceneName, LoadSceneMode loadSceneMode, TMachine machine, T argument = default)
            where TState : State<TMachine>
        {
            LoadingProcessor.Instance.RegisterLoadingModel<TMachine, TState, T>(machine, argument);

            return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }
    }
}
