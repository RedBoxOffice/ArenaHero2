namespace Base.TypedScenes
{
    using Base.StateMachine;
    using UnityEngine.SceneManagement;


    public class Initialize : TypedScene<GameStateMachine>
    {
        private const string _sceneName = "Initialize";

        public static void Load<TState>(GameStateMachine machine) where TState : State<GameStateMachine>
        {
            LoadScene<TState>(_sceneName, LoadSceneMode.Single, machine);
        }

        public static void Load(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            LoadScene(_sceneName, loadSceneMode);
        }

        public static UnityEngine.AsyncOperation LoadAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return LoadScene(_sceneName, loadSceneMode);
        }
    }
}