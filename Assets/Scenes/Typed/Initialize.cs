namespace Base.TypedScenes
{
    using UnityEngine.SceneManagement;


    public class Initialize : TypedScene
    {
        private const string _sceneName = "Initialize";

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